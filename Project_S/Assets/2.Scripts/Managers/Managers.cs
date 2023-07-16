using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ӳ��� ��� �Ŵ����� ��Ʈ���ϱ� ���� Ŭ����

public class Managers : ManagerBase
{
    public static GameManager _game;
    public static SceneManager _scene;
    public static ResourceManager _resource;
    public static SoundManager _sound;
    public static EventManager _event;

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        _scene = Init<SceneManager>();
        _resource = Init<ResourceManager>();
        _sound = Init<SoundManager>();
        _game = Init<GameManager>();
        _event = Init<EventManager>();

        DontDestroyOnLoad(this.gameObject);
    }

    private T Init<T>() where T : ManagerBase
    {
        T manager = GetComponent<T>();

        if (manager == null)
        {
            manager = gameObject.AddComponent<T>();
        }
        manager.Init();

        return manager;
    }
}
