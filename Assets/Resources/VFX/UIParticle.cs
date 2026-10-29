
using UnityEngine;
using Zenject;

[RequireComponent(typeof(RectTransform))]
public class UIParticle : MonoBehaviour
{
	private RectTransform RectTransformComponent;
	private Vector2 Direction;
	private float Speed;
	private float Lifetime;
	private float ShrinkStart;
	private float Elapsed;
	private Vector3 initialScale;

	private void Awake()
	{
		RectTransformComponent = GetComponent<RectTransform>();
	}

	public void Spawn(Vector2 direction, float speed, float lifetime, float shrinkStart)
	{
		Direction = direction;
		Speed = speed;
		Lifetime = lifetime;
		ShrinkStart = shrinkStart;

		Elapsed = 0f;
		initialScale = RectTransformComponent.localScale;
	}

	private void Update()
	{
		Elapsed += Time.deltaTime;

		RectTransformComponent.anchoredPosition += Direction * Speed * Time.deltaTime;

		float progress = Elapsed / Lifetime;

		if (progress >= ShrinkStart)
		{
			float t = Mathf.InverseLerp(ShrinkStart, 1f, progress);
			RectTransformComponent.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
		}

		if (Elapsed >= Lifetime)
			Destroy(gameObject);
	}

	public class UIParticleFactory : PlaceholderFactory<UIParticle> { }
}