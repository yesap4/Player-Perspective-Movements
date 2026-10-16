# Player Perspective Movements

This Unity project contains two movement styles in separate scenes: a first-person controller and a third-person controller. The goal is to explore how perspective changes movement feel, camera behavior, and character control.

## First Person Perspective (FPP)

This scene uses a first-person camera setup with mouse look and player movement controlled by WASD. The view sits at the character's eye level, creating an immersive, close-range gameplay feel.


## Third Person Perspective (TPP)

This scene uses a third-person camera and character controller with smooth turning and movement direction based on the character's facing. It is better suited for wide-level traversal and character visibility.


## Project Structure

- `Assets/Scenes/FPP.unity` ? first-person scene
- `Assets/Scenes/TPP.unity` ? third-person scene
- `Assets/PlayerMovement.cs` ? first-person controller script
- `Assets/ThirdPersonMovement.cs` ? third-person controller script
- `Assets/MouseLook.cs` ? mouse-based camera rotation for the first-person view

## Controls

- Move: `W`, `A`, `S`, `D`
- Jump: `Space`
- Look around: mouse movement

## How to clone only the build folder

If you want to keep just the runnable build without the full Unity project, you can clone only the `Builds` folder from GitHub using a sparse checkout.

1. Open a terminal in an empty folder.
2. Run:

```bash
git init
 git remote add origin https://github.com/yesap4/First-Player-Movement.git
 git sparse-checkout init --cone
 git sparse-checkout set Builds
 git pull origin main
```

3. After the command finishes, the `Builds` folder will be available locally.
4. Open the executable inside the build folder to play the game.

For example, the packaged Windows build is available under `Builds/FPP/v1.0.0/`.

## Run the project

1. Open the project in Unity.
2. Load the `FPP` or `TPP` scene.
3. Press Play to test the different movement perspectives.
