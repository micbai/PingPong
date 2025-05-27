
A Unity programming exercise

### To create a dashed line using a texture on a Line Renderer in Unity:

#### 1. Prepare the Dashed Texture
- Create a simple dashed-line texture in an image editor (alternatively, you can find one online).
- Ensure the texture has transparency where the gaps should be.
- Save it as a **PNG** file.
#### 2. Import the Texture into Unity
- Drag the texture into Unity’s **Assets** folder.
- Select the texture and set **Wrap Mode** to **Repeat**.
- Set **Filter Mode** to **Point** (for sharp edges).
#### 3. Create a Material for the Line Renderer
- Right-click in the **Assets** folder and select **Create > Material**.
- Assign the dashed texture to the material’s **Albedo** property.
- Set the **Shader** to **Unlit/Transparent** or **Legacy > Transparent > Diffuse**.
#### 4. Apply the Material to the Line Renderer
- Add a **Line Renderer** component to your GameObject.
- Assign the material to the **Material** field in the Line Renderer.
- Adjust the **Tiling** in the material settings to control the dash spacing.
#### 5. Fine-Tune the Line Renderer
- Set the **Texture Mode** to **Tile**.
- Adjust the **Width** and **Positions** to fit your needs.

### Add a bouncing effect to a ball:

Use Unity's built-in 2D physics system, specifically **Rigidbody2D** and **Physics Material 2D**. This approach leverages the physics engine to handle collisions and realistic bouncing, rather than you having to write complex custom physics calculations.
#### 1. Set Up Your Ball GameObject
Add  a 2D ball GameObject. In the Inspector Hirarchy, add a the ball component -> `2D Object` -> `Sprites` -> `Square`. The square has a sprite renderer component attached already, we just need to add the Collider 2S and the Rigidbody.
* **Sprite Renderer:** To display the ball's image.
* **Collider2D:** This defines the ball's shape for collision detection. For our square ball, we use a **BoxCollider2D**.
* **Rigidbody2D:** This is the core component that brings your ball under the control of Unity's physics engine.
    
#### 2. Create a Physics Material 2D
A Physics Material 2D allows you to define properties like friction and bounciness for objects when they collide.
1.  In your Project window, right-click -> `Create` -> `2D` -> `Physics Material 2D`.
2.  Name it something like "Bouncy".
3.  Select the "Bouncy" in the Project window.
4.  In the Inspector, you'll see two main properties:
    * **Friction:** This controls how much objects rub against each other during collision. For a bouncy ball, you'll generally want a low friction value (e.g., 0 or close to 0) to prevent it from slowing down too much horizontally.
    * **Bounciness (or Restitution):** This is the key property for bouncing.
        * A value of **0** means no bounce (objects stick together).
        * A value of **1** means a perfect bounce (no energy loss, bounces to the same height).
        * Values between **0 and 1** represent varying degrees of energy loss, making the ball bounce lower over time.
    * For a highly bouncy ball, set **Bounciness** to `1`. If you want it to eventually settle, set it to something like `0.8` or `0.9`.
#### 3. Apply the Physics Material 2D to Your Ball
Now, tell your ball to use this bouncy material:
1.  Select your ball GameObject in the Hierarchy.
2.  In the Inspector, find its **BoxCollider 2D** component.
3.  You'll see a field called "Material". Drag your "Bouncy" physics material from the Project window into this "Material" field.

#### 4. Ensure Other Colliders are Set Up
For your ball to bounce, it needs to collide with other objects that also have **Collider2D** components (e.g., ground, walls, other obstacles).
* Make sure your ground, walls, and any other surfaces your ball will hit also have 2D colliders attached (e.g., `BoxCollider2D`, `EdgeCollider2D`).
* These static environmental colliders **do NOT need Rigidbody2D components**. They are considered static objects in the physics simulation.

#### 5. Adjust Project Physics Settings (Optional but Recommended)
There are some global physics settings that can affect bouncing:
1.  Go to `Edit` -> `Project Settings` -> `Physics 2D`.
2.  **Gravity:** Adjust the `Y` value for gravity if you want your ball to fall faster or slower. The default is typically -9.81 (standard Earth gravity).
3.  **Velocity Threshold:** This is an important setting. By default, it's often `1`. If two colliding objects have a relative velocity below this value, they will *not* bounce. If your ball seems to stop bouncing at low speeds, try reducing this value (e.g., to `0.1` or even `0.0001`). This ensures that even slow collisions result in a bounce.

##### How it Works:
When your ball (with its Rigidbody2D and Collider2D + Physics Material 2D) collides with another collider, Unity's physics engine automatically calculates the bounce based on the `Bounciness` value of the Physics Material 2D assigned to the ball's collider. The Rigidbody2D then applies the necessary forces to simulate the bounce.
#### 6. Adding Initial Movement
You'll need to give the ball  an initial force to get it moving. You can do this with a simple script:

```csharp
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public float launchForce = 5f;
    public Vector2 launchDirection = new Vector2(1, 0).normalized; // Launch horizontal right
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Apply an initial force to the ball

        if (rb != null)
        {
            rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
        }
    }
}
```


1. Create a new C# script (e.g., `Ball`).
2. Attach it to the ball GameObject.
3. Adjust `Launch Force` and `Launch Direction` in the Inspector to give your ball the desired initial push. `ForceMode2D.Impulse` is good for instant, immediate force.

