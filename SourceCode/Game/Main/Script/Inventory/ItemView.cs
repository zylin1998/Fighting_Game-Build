using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

namespace FightingGame
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _Renderer;
        [SerializeField]
        private float          _ExistTime;
        [SerializeField]
        private float          _FadeDuration;

        public MemoryPool<ItemView> Pool { get; set; }

        private GameObject _Root;

        private void Awake()
        {
            if (!_Root) 
            {
                _Root = GameObject.Find("Item") ?? new GameObject("Item");
            }

            transform.SetParent(_Root.transform);
        }

        public void OnSpawn()
        {
            gameObject.SetActive(true);
            
            _Renderer.color = Color.white;

            Observable
                .Timer(TimeSpan.FromSeconds(_ExistTime))
                .Subscribe(t =>
                {
                    _Renderer?
                        .DOFade(0, _FadeDuration)
                        .OnComplete(() =>
                        { 
                            Pool.Despawn(this); 

                            gameObject.SetActive(false);
                        });
                });
        }
    }
}