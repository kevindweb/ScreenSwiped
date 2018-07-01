# MakeItBounce

Slide across the screen, creating angled platforms to avoid falling to your death! Bounce past enemies and pick up abilities along the way. How far will you get?...

## Getting Started

This is the development version of the project, but a built solution is soon to come!

### Prerequisites

If you want to install and run on your own, please install [Unity](https://unity3d.com/) and learn some C#!

```
public GameObject me;
void Update(){
	if(me.eyesBleeding)
		Debug.Break();
	else
		continue;
}
```

## Testing

Hit me up on my [email](mailto:kevin8deems@gmail.com) to help me debug and test my game, if you want

### And coding style tests

Unity coding is all about scripting (I use C#), where small snippets of code apply to different components (objects) in the game

```
void FixedUpdate(){
	rg2d.AddForce(new Vector2(horizontalGravity, 0));
	// make horizontal gravity
	if(magnetField.Count != 0){
		float step = magnetForce * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position, magnetField[1].position, step);
	}
}
// this code pulls player towards ground, and initializes the magnet platform
```

## Contributing

Email me to help work on the game, and I'll see what's going on!


## Authors

* **Kevin Deems** - *Creator* - [Portfolio site](https://kevindweb.github.io)

See also the list of [contributors](https://github.com/kevindweb/ScreenSwiped/contributors) who participated in this project.


## Acknowledgments

* Tip of the hat to my favorite game [The Binding Of Isaac](http://bindingofisaac.com/) for inspiration
* Thanks to all my fam and friends who have been helping test for me already!
