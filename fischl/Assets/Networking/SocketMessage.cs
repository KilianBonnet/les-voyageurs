using System.Reflection;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SocketOP
{
    public const int ERROR_EVENT = 0;
    public const int HELLO_EVENT = 1;
    public const int IDENTIFY_EVENT = 2;
    public const int READY_EVENT = 3;

    public const int SCENE_CHANGE_EVENT = 10;
    public const int INVOKE_EVENT = 11;
    public const int SCORE_EVENT = 12;
    public const int ROOM_EVENT = 13;
    public const int BONUS_EVENT = 14;
    public const int TRANSFORM_EVENT = 15;
    public const int SELECT_ENV_COMPLETE_EVENT = 16;
}

public class SocketMessage
{
    public int op { get; set; }
    public JObject d { get; set; }
}

public class SceneChangeData
{
    public string scene { get; set; }
}

public class InvokeData
{
    public int networkObjectId;
}

public class ScoreData
{
    public string type;
    public int score;
}

public class RoomData
{
    public int room;
}

public class BonusData
{
    public int bonus;
}

public class TransformData
{
    public int networkObjectId;
    public SerializedVector3 position;
    public SerializedVector3 rotation;
}

public class SerializedVector3
{
    public float x;
    public float y;
    public float z;

    public SerializedVector3(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}