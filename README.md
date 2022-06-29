# AnimFlex

An open source project to solve the problem of having to create performant inspectable sequences. There's Timeline now, but it's not as performant, and as designer friendly as one would like it to be; to create a custom event, you have to go through hell to get it there and ready to use for designers. 
AnimFlex is a super-performant array-based sequencing solution to create custom event-based clips on the fly.

# Installation
Get the UnityPackage for free from https://github.com/somedeveloper00/AnimFlex/releases/download/main/AnimFlexPackage.unitypackage

# Manual
* you create a "Clip Sequence Component"

![image](https://user-images.githubusercontent.com/79690923/175804646-549ec686-723c-481b-b5b4-49c5343a981f.png)
* then click on the Add button and add new clip node

![image](https://user-images.githubusercontent.com/79690923/176524993-a4519ca2-22fe-4e4f-9db1-c6c37a16a8a3.png)
* for example, I added a "log" clip node

![image](https://user-images.githubusercontent.com/79690923/176525506-608da154-9c7b-499a-8f44-2ab221ae45a1.png)
* Then obviously you can rename, add delay, tweak parameters for your clip node and add next nodes that'll play after this finishes.
- You can also choose to change it's color if you wished to

![image](https://user-images.githubusercontent.com/79690923/176526857-9c9526bb-f5fb-4f42-9281-ae82a64c32e4.png)
![image](https://user-images.githubusercontent.com/79690923/176528303-063ce5fb-b3aa-4165-b349-2a75eadaaa52.png)
* Let's add a new log node and make it log "I'm the second node!" and play 3 seconds after the first node:

![image](https://user-images.githubusercontent.com/79690923/176529036-b1f25873-a54d-48cc-bf61-ae99a1dd9f39.png)
* Now to make the 2nd node play after the 1st one finishes, you can either turn on "Play Next" toggle on the 1st node, or add the 2nd node to the "Next Clip Nodes" list

![image](https://user-images.githubusercontent.com/79690923/176529678-3a422f13-0044-444f-b097-d0507041c654.png)
* and play and check the results inside the Console

![image](https://user-images.githubusercontent.com/79690923/176531697-549d23f0-8f47-4539-845c-0a210c5eb837.png)
* That's it! 
* You can also add function calls (UnityEvent) as a clip too, and call your custom functions in your custom scripts. up to 3 argumental events are valid so far

![image](https://user-images.githubusercontent.com/79690923/176532431-50be90bf-7f88-4034-aff4-d102f081b898.png)

* As a bonus, you can also group your clips together to preserve some inspector space!
(excuse my handwriting. to be fair, it's on mouse; but the buttons should look pretty self-epxlanatory!)

![image](https://user-images.githubusercontent.com/79690923/176535647-9125e41e-cb7a-46b1-97fa-f55fb2147f91.png)


# Add custom Clips
* Create a new class that derives from Clip
- You can write your custom logics inside OnStart, and add custom serialized fields inside the class itself to get inputs from inspector. 
For this example, let's just say hi to whoever is calling this clip (note that you have to call `End()` after you're done with your logic)

```csharp
using AnimFlex.Clipper;
using UnityEngine;

public class MyCustomClip : Clip
{
    [SerializeField] private string yourName = "Someone";

    protected override void OnStart()
    {
        Debug.Log($"Hi {yourName}");
        End();
    }
}
```


![image](https://user-images.githubusercontent.com/79690923/176534278-02b5939e-a471-488e-8210-d87fa26bab1c.png)


* Now a more useful clip perhaps, huh? one that, for example, can take DoTween animations and play them all at once with predefined delay in a linear way 
-> the image taken is from an older version

![image](https://user-images.githubusercontent.com/79690923/175806488-abd189bf-a1dd-404b-a837-2f6f197080be.png)
![image](https://user-images.githubusercontent.com/79690923/175806490-f5dcce48-112f-4c38-b181-36dfff86c805.png)
![sample](https://user-images.githubusercontent.com/79690923/175806506-16519f3c-f691-4a3f-a94a-d4ee17ae7374.gif)


