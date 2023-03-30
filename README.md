# AnimFlex
### A fast (faster than DOTween) and simple to use Sequencer & Tweener.
![image](https://user-images.githubusercontent.com/79690923/228853422-6c74dee5-ead2-48e3-9edd-0859c01a54f9.png)

## Systems
### Tweener
Tweener is a tweening system that makes things go from point A to point B. It could be animating the position of an object, rotation of an object, intensity of a light, size of a text or even just a simple float field. 
### Sequencer
Sequencer is a system that plays it's *Clips* from start to finish, in order and in predefined time. It's like a timeline, but it's designed to be modular, and store all data inside and as a component for small animations/clips. (it's NOT designed to be a replacement to timeline)

## Availablility and Usage
### Tweeners
Transform manipulations (Position, Rotation, Scale)
```csharp
transform.AnimPositionTo( new Vector3( 10, 0, 10 ));
```
* Color (for `Renderer`, `Graphic`, `Image`, `Material`, `TMP_Text`, `Text` and `Light` Objects)
```csharp
GetComponent<MeshRenderer>().AnimColorTo( Color.red);
```
* Fade (for `Renderer`, `Graphic`, `Image`, `Material`, `TMP_Text`, `Text` and `Light` Objects)
```csharp
GetComponent<MeshRenderer>().AnimFadeTo( Color.red);
```
* Text interpolation (for Textmesh and legacy Text)
```csharp
GetComponent<TMP_Text>().AnimTextTo( "Hello World" );
```
* Light Intensity
```csharp
GetComponent<Light>().AnimLightIntensityTo( 0.1f );
```
* Light Range
```csharp
GetComponent<Light>().AnimLightRangeTo( 5.5f );
```
* Projector Size
```csharp
GetComponent<Projector>().projector.AnimProjectorSizeTo( 2.5f );
```
* Projector Aspect Ratio
```csharp
GetComponent<Projector>().projector.AnimProjectorAspectRatioTo( 1.5f );
```
* Projector Field of View
```csharp
GetComponent<Projector>().projector.AnimProjectorFieldOfViewTo( 40f );
```

### Clips
* All of the tweeners. They have a normal and a *multi* clip; the *multi* tweener clips apply a tweener to multiple targets with the given set of rules.
![image](https://user-images.githubusercontent.com/79690923/228864137-fc660ed8-c79b-4114-b51c-4a1c588d754f.png)
* Unity Events (primitive aruments up to 3). You can easily expand the system to add an event clip with your special argument type(s)
![image](https://user-images.githubusercontent.com/79690923/228865336-8c151721-9c4b-4752-af27-e011db1855b5.png)
* Branching. **goto** and **if statement** (if statements support primitive conditions. you can expand that)
![image](https://user-images.githubusercontent.com/79690923/228866194-028596c4-72f0-495f-b45a-427a62a28dc1.png)
![image](https://user-images.githubusercontent.com/79690923/228866630-bf484cfd-0f4e-4059-bafb-731814c23bbe.png)
* Log. Performs a simple Unity console log
![image](https://user-images.githubusercontent.com/79690923/228866988-ec41fa8b-9349-46f2-922d-21ca3fcb550b.png)
* Set Value. They can modify a value from a given component thorugh reflection. (They support primitive value types, but you can easily expand that)
![image](https://user-images.githubusercontent.com/79690923/228867603-0dd1ff1d-d529-490c-990f-2d6c73d1056a.png)
* Wait Until. They wait for the given field to meed a specific condition. (They support primitive types and basic equal oprtator, but you can easily expand that)
![image](https://user-images.githubusercontent.com/79690923/228868805-0ffdcc3b-afe1-436d-a136-dfa0b6c70e44.png)
* Misc. They're helper clips, i.e. **Empty** clip does nothing and can be used to make notes, and **End** finishes the clip on reach.
![image](https://user-images.githubusercontent.com/79690923/228869776-d704a1c2-bf93-4941-bd78-e88522cdd9bc.png)


## Youtube samples :
https://www.youtube.com/watch?v=QNxzgGmmYhQ  
https://www.youtube.com/watch?v=lWghzbCR2ds  
https://www.youtube.com/watch?v=e2mkIyX8hEY
