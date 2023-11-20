# Groupe Les Voyageurs
<br>
Membres du groupe<br><br>
BONNET Kilian <br>
BOUZIDI Héba<br>
CHABANIER Aurélia<br>
DEVICTOR Pauline<br>
<br>
Projet sur table tactile avec casque VR



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