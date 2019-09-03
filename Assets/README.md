# Air-Sea Battle

* A component based architecture has been used. This means using logic as components instead of everything in a single script using inheritance. For example, Plane has Motor, Health and InScreenObject components, and the Plane class is in charge of connecting them.

 
* Object Pools have been implemented in order to prevent instantiation. A general ObjectPool<T> has been implemented and pools like PlanePool and BulletPool inherit from it.


* Same thing happens for Spawners.
* To show the potential of the component based architecture and the general pools and spawners, Clouds have been implemented. It was just needed to add Motor, InScreenObject and ObjectPool/Spawner.


* A Loading Scene has also been implemented for different reasons. It isn't really needed as this game loads pretty quickly, but it can be handful for async operations like the Data retrieving via HTTP, or even scenes with lots of pools that have to instantiate a lot of objects when being loaded.


* A game pause screen has being implemented when pressing "Escape button".

* Scriptable Objects have been used to store Game Configuration or scores.

* Singletons have been used for managers like GameManager, AudioManager or SceneLoader.

* Gameflow:  Loading Scene -> Menu Scene -> Loading Scene -> Game Scene and viceversa

* Data retrieving is made the first time the Loading Scene is loaded.

* Estimate hours: 20-22

* 3rd party assets:
- Arcade Classic Font by Pizzadude - https://www.1001fonts.com/arcadeclassic-font.html
- Shooting sound - Freesound.org
- Button Arcade sound - Freesound.org
- Arcade music - Freesound.org