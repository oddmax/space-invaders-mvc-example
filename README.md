# space-invaders-mvc-example
A simple space invaders games demonstrating MVC-like architecture based on Zenject and UniRX with Command pattern

## Funcionality

Supports different enemy types with different damage and lives values.
Supports different levels with possibility to arrange enemies and their types.

## Main structure

Uses very simple MVC/MVP architecture with some extra flavor of UniRX for nicer update of UI (ReactiveProperties) and Command pattern
Uses Zenject of inversion of control and dependencies injections
Uses SignalBus from Zenject to decouple communication between different components

# Models 
* Is Plain C# class
* Holds application state
* Injectable into Presenters, Commands and Services (in case of adding backend). Not injectable to other models.
* Doesn't have injected dependencies
* Has DataProvider interface for get-only access (created as-needed by views)

# Views
* Is Subclass of MonoBehaviour
* Can have references to a group of View Elements and child Views that share the same purpose
* Listens to user input on the view elements and forwards them to the Presenter
* Not injectable
* Doesn't have injected dependencies

# Presenter
vHas references to Views
* Listens to changes in Models and updates the Views it is responsible for
* Has injected dependencies to Models, SignalBus, and Services.

# Systems
* Is Plain C# class
* Listens to user input or executes inner logic of movement or attack. 
* Depends and belongs to presenter. 
* Done in order to move responsibilites dependent on Unity engine (access to rigid bodies and physics) to a separate class with a single responsibility. 

# Config(Definition) located in StaticData folder
* Is a ScriptableObject
* Gets serialized 
* Holds static information of the objects which doesn't change such as their damage values, lifes, how enemies are located on the level, etc

# Signals
* Is Plain C# class
* Used to notify other parts of the system of models updates or to execute a command

# Command
* Is Plain C# class
* Micro controller responsible to execute one action and update all necessary models based on result of this action



## What's next

I didn't have time but next step would be to add basic Unit testing for Model classes.
Another thing which I would like to do is to recreate the same example but with clean ECS architecture.
