# AnimFlex

An open source project to solve the problem of having to create performant inspectable sequences. There's Timeline now, but it's not as performant, and as designer friendly as one would like it to be; to create a custom event, you have to go through hell to get it there and ready to use for designers. 
AnimFlex is a super-performant array-based sequencing solution to create custom event-based clips on the fly.

# Manual
* you create a "Clip Sequence Component"

![image](https://user-images.githubusercontent.com/79690923/175804646-549ec686-723c-481b-b5b4-49c5343a981f.png)
* then click on the Add button and add new clip node

![image](https://user-images.githubusercontent.com/79690923/175804683-647963e8-1fef-431c-8b3a-c11e571ee559.png)
* for example, I added a "log" clip node

![image](https://user-images.githubusercontent.com/79690923/175804741-3faa8cb0-720e-434a-9eec-bd4d803d2011.png)
* Then obviously you can rename, add delay, tweak parameters for your clip node and add next nodes that'll play after this finishes

![image](https://user-images.githubusercontent.com/79690923/175804791-a200d942-830a-4681-97b5-3085cf2cc3ca.png)
* Let's add a new log node and make it log "I'm the second node!" and play 3 seconds after the first node:

![image](https://user-images.githubusercontent.com/79690923/175804848-6ea7371a-2a92-4f53-8b21-d5d270063f60.png)
* Now to make the 2nd node play after the 1st one finishes, you can either turn on "Play Next After Finish" toggle on the 1st node, or add the 2nd node to the "Next Clip Nodes" list

![image](https://user-images.githubusercontent.com/79690923/175804918-bab7e97c-23c4-426b-bb6c-cfa1118cf4a7.png)
![image](https://user-images.githubusercontent.com/79690923/175804920-b4cad61c-3c1a-4211-ae02-011cfd5518b5.png)
* and play and check the results inside the Console

![image](https://user-images.githubusercontent.com/79690923/175804934-2309efef-c78b-487c-9ffc-0c494931c7d8.png)
* That's it! 
* Now you can also add function calls (UnityEvent) as a clip too, and call your custom functions in your custom scripts. up to 3 argumental events are valid so far

![image](https://user-images.githubusercontent.com/79690923/175805014-1bf0eab6-0b83-458c-b77b-facddab7b026.png)

# Add custom Clips
* Create a new class that derives from Clip

![image](https://user-images.githubusercontent.com/79690923/175805081-72562db6-0c51-4dc7-a9c8-92d4a21b654c.png)
* You can write your custom logics inside OnStart, and add custom serialized fields inside the class itself to get inputs from inspector. 
For this example, let's just say hi to whoever is calling this clip (note that you have to call `End()` after you're done with your logic)
![image](https://user-images.githubusercontent.com/79690923/175806987-2a47644b-d8bc-4947-a3be-62d25d9e6674.png)
![image](https://user-images.githubusercontent.com/79690923/175806992-8afcef38-8ad9-491a-813d-992dc8af34be.png)


![image](https://user-images.githubusercontent.com/79690923/175805170-287ed29d-f660-42a1-b19d-474b60e8f8e0.png)
* Now a more useful clip perhaps, huh? one that, for example, can take DoTween animations and play them all at once with predefined delay in a linear way

![image](https://user-images.githubusercontent.com/79690923/175806488-abd189bf-a1dd-404b-a837-2f6f197080be.png)
![image](https://user-images.githubusercontent.com/79690923/175806490-f5dcce48-112f-4c38-b181-36dfff86c805.png)
![sample](https://user-images.githubusercontent.com/79690923/175806506-16519f3c-f691-4a3f-a94a-d4ee17ae7374.gif)


