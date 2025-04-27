using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Util {
    public static class Extensions {
        public static async UniTask ToUniTask(this Tween tween, TweenCallback onUpdate = null) {
            while (tween.active && !tween.IsComplete()) {
                onUpdate?.Invoke();
                await UniTask.Yield();
            }
        }

        public static void ChangeCollectionParent<T>(this IEnumerable<T> collection, Transform parent, Predicate<T> predicate = null) where T : Component {
            using var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext()) {
                if (!enumerator.Current) continue;
                if (predicate != null && !predicate(enumerator.Current)) continue;
                enumerator.Current.transform.SetParent(parent, true);
            }
        }
    }
}