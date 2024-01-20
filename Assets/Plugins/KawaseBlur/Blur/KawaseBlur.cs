using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class KawaseBlur : ScriptableRendererFeature
{
	public static KawaseBlur Instance;

	public KawaseBlurSettings settings = new KawaseBlurSettings();

	private CustomRenderPass scriptablePass;
	private Material m_UIMaterial;

	public override void Create()
	{
		Instance = this;
		this.SetActive(false);

		if (scriptablePass != null)
		{
			scriptablePass.Deinit();
		}

		scriptablePass = new CustomRenderPass(settings);

		if (m_UIMaterial != null)
		{
			m_UIMaterial.DestroySafe();
		}

		m_UIMaterial = Instantiate(settings.UIMaterial);
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		var src = renderer.cameraColorTarget;
		scriptablePass.Setup(src);
		renderer.EnqueuePass(scriptablePass);
	}

	public Material GetUIMaterial()
	{
		return m_UIMaterial;
	}

	public IEnumerator RenderBlur(Color color)
	{
		scriptablePass.SetColor(color);
		this.SetActive(true);

		yield return null;

		this.SetActive(false);
	}

	public void SetTarget(Type descriptor)
	{
		scriptablePass.SetTarget(descriptor);
	}

	public void ReleaseTarget(Type descriptor)
	{
		scriptablePass.ReleaseTarget(descriptor);
	}

	public void SetGlobalTexture(Type descriptor)
	{
		scriptablePass.SetGlobalTexture(descriptor);
	}

	[System.Serializable]
	public class KawaseBlurSettings
	{
		public string ProfilerTagName = "KawaseBlur";
		public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRendering;
		public Material blurMaterial;
		public Material blurMaterialColor;
		public Material UIMaterial;

		[Range(2,15)]
		public int blurPasses = 1;

		[Range(1,4)]
		public int downsample = 1;
		public bool copyToFramebuffer;
		public string targetName = "_blurTexture";
	}

	class CustomRenderPass : ScriptableRenderPass
	{
		private Material blurMaterial;
		private Material blurMaterialColor;
		private int passes;
		private int downsample;
		private bool copyToFramebuffer;
		private int targetNameID;
		private int m_OffsetKawaseBlurID;
		private int m_ColorID;
		private string profilerTag;

		private int tmpId1;

		private RenderTargetIdentifier tmpRT1;
		private TargetDescriptor tmpRT2;
		private int m_Width;
		private int m_Height;

		private Type m_CurrentDescriptor;
		private Dictionary<Type, TargetDescriptor> m_RenderTextures = new Dictionary<Type, TargetDescriptor>(4);

		private RenderTargetIdentifier source { get; set; }

		public void Setup(RenderTargetIdentifier source)
		{
			this.source = source;
		}

		public void SetColor(Color color)
		{
			blurMaterialColor.SetColor(m_ColorID, color);
		}

		public CustomRenderPass(KawaseBlurSettings settings)
		{
			profilerTag = settings.ProfilerTagName;
			blurMaterial = settings.blurMaterial;
			blurMaterialColor = Instantiate(settings.blurMaterialColor);
			passes = settings.blurPasses;
			downsample = settings.downsample;
			copyToFramebuffer = settings.copyToFramebuffer;
			targetNameID = Shader.PropertyToID(settings.targetName);
			m_OffsetKawaseBlurID = Shader.PropertyToID("_offsetKawaseBlur");
			m_ColorID = Shader.PropertyToID("_Color");

			renderPassEvent = settings.renderPassEvent;
		}

		public void Deinit()
		{
			blurMaterialColor.DestroySafe();

			foreach (var renderTexture in m_RenderTextures)
			{
				renderTexture.Value.RenderTexture.DestroySafe();
				renderTexture.Value.RenderTexture = null;
			}
			m_RenderTextures.Clear();
		}

		public void SetTarget(Type descriptor)
		{
			m_CurrentDescriptor = descriptor;
		}

		public void ReleaseTarget(Type descriptor)
		{
			if (m_RenderTextures.TryGetValue(descriptor, out var identifier))
			{
				identifier.RenderTexture.DestroySafe();
				identifier.RenderTexture = null;
			}

			m_RenderTextures.Remove(descriptor);
		}

		public void SetGlobalTexture(Type descriptor)
		{
			if (m_RenderTextures.TryGetValue(descriptor, out var identifier))
			{
				Shader.SetGlobalTexture(targetNameID, identifier.RenderTexture);
			}
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			m_Width = cameraTextureDescriptor.width / downsample;
			m_Height = cameraTextureDescriptor.height / downsample;

			tmpId1 = Shader.PropertyToID("tmpBlurRT1");
			cmd.GetTemporaryRT(tmpId1, m_Width, m_Height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32);

			tmpRT1 = new RenderTargetIdentifier(tmpId1);

			if (m_RenderTextures.TryGetValue(m_CurrentDescriptor, out var identifier) == false)
			{
				identifier = new TargetDescriptor();
				identifier.RenderTexture = new RenderTexture(m_Width, m_Height, 0, RenderTextureFormat.ARGB32);
				identifier.RenderTargetIdentifier = new RenderTargetIdentifier(identifier.RenderTexture);
				m_RenderTextures.Add(m_CurrentDescriptor, identifier);
			}

			tmpRT2 = identifier;

			ConfigureTarget(tmpRT1);
			ConfigureTarget(tmpRT2.RenderTargetIdentifier);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

			RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
			opaqueDesc.depthBufferBits = 0;

			// first pass
			// cmd.GetTemporaryRT(tmpId1, opaqueDesc, FilterMode.Bilinear);
			cmd.SetGlobalFloat(m_OffsetKawaseBlurID, 1.5f);
			cmd.Blit(source, tmpRT1, blurMaterialColor);

			for (var i = 1; i < passes - 1; i++)
			{
				cmd.SetGlobalFloat(m_OffsetKawaseBlurID, 0.5f + i);
				cmd.Blit(tmpRT1, tmpRT2.RenderTargetIdentifier, blurMaterial);

				// pingpong
				var rttmp = tmpRT1;
				tmpRT1 = tmpRT2.RenderTargetIdentifier;
				tmpRT2.RenderTargetIdentifier = rttmp;
			}

			// final pass
			cmd.SetGlobalFloat(m_OffsetKawaseBlurID, 0.5f + passes - 1f);
			if (copyToFramebuffer)
			{
				cmd.Blit(tmpRT1, source, blurMaterial);
			}
			else
			{
				cmd.Blit(tmpRT1, tmpRT2.RenderTargetIdentifier, blurMaterial);
				//cmd.SetGlobalTexture(targetNameID, tmpRT2.RenderTexture);    
			}

			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();

			CommandBufferPool.Release(cmd);
		}

		public override void FrameCleanup(CommandBuffer cmd)
		{

		}

		class TargetDescriptor
		{
			public RenderTexture RenderTexture;
			public RenderTargetIdentifier RenderTargetIdentifier;
		}
	}
}

public static class ObjectExtensions
{
	public static void DestroySafe(this UnityEngine.Object @this)
	{
		if (@this == null)
			return;

#if UNITY_EDITOR
		if (Application.isPlaying == true)
		{
			UnityEngine.Object.Destroy(@this);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(@this);
		}
#else
		UnityEngine.Object.Destroy(@this);
#endif
	}
}
