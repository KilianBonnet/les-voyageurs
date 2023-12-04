# Groupe Les Voyageurs
### Membres du groupe
BONNET Kilian <br>
BOUZIDI Héba<br>
CHABANIER Aurélia<br>
DEVICTOR Pauline<br>

## File structure
| File name | Description |
| --------- | ----------- |
| fishl | The Unity app for the tactile table. |
| akasha | The server allowing the table and the VR Headset to communicate. |
| nahida | The Unity app for the VR Headset. |



## Socket
### Codes
| Opcode | Description | Payload |
| ------ | ----------- | ------- |
| 0 | Error event | message |
| 1 | Hello event | / |
| 2 | Identify Event | device |
| 3 | Ready Event | / |

### 1 - Establish connection with Server
Establish connection with [ws://localhost:8080](ws://localhost:8080)

If the connection is successful, you'll receive an `Hello Event`.

#### Example of Hello Event
```
{
  "op": 1
}
```

### 2 - Identify your device to the socket
Once the Hello Event successful you'll need to identify your device to the server.

#### Example Identify Payload
```
{
  "op": 2,
  "d": {
    "device": "VR_Headset"
  }
}
```
#### Currently supported devices
| Device | Description |
| ------ | ----------- |
| VR_Headset | The Unity client associated to the VR headset app. |
| Table | The Unity client associated to the Table app. |

### 3 - Ready Event
Once the device identified, you'll receive an `Ready Event`. 

#### Example of Ready Event
```
{
  "op": 3
}
```

### Scene Change Event
Event sent when the client need to change the scene to the given scene.

#### Example of Scene Change Event
```
{
  "op": 10,
  "d": {
    "scene": "Bowrelia"
  }
}
```

### Invoke Event
Event sent when the client want to Invoke a function through the network.

#### Example of Invoke Event
```
{
  "op": 11,
  "d": {
    "networkObjectId": 121123
  }
}
```
### Score Event
Event sent when the client want to update the score.

#### Example of Invoke Event
```
{
  "op": 12,
  "d": {
    "type": "increase"
    "score": 123
  }
}
```

| Type | Description |
| --------- | ----------- |
| increase | The amount to increase the score by. |
| info | The current score. |