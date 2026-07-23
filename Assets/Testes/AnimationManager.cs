using Cysharp.Threading.Tasks;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    private UniTaskCompletionSource completion;

    public const string idleAnimation = "idlePlayer";
    public const string attackAnimation = "attackPlayer";
    public const string returnAnimation = "returnPlayer";
    public const string hitAnimation = "hitPlayer";


    public async UniTask Play(string state)
    {
        completion = new UniTaskCompletionSource();

        animator.CrossFade(state, 0.1f);

        await completion.Task;
    }

    public void AnimationFinished()
    {
        Debug.Log("Animation Finished");

        completion?.TrySetResult();
    }

    public void IdleAnimation()
    {
        animator.Play(idleAnimation);
    }

    public async UniTask AttackAnimation()
    {
        await Play(attackAnimation);
    }

    public async UniTask ReturnAnimation()
    {
        await Play(returnAnimation);
    }

    public async UniTask HitAnimation()
    {
        await Play(hitAnimation);
    }
}
