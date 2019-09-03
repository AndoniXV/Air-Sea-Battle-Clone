using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class in charge of controlling the player input and connecting every component of the player.
/// </summary>
[RequireComponent(typeof(Launcher))]
public class PlayerController : MonoBehaviour
{
	private Launcher launcher;
	private Animator anim;
	private bool locked;

	private Vector2 shootingDirection1;
	private Vector2 shootingDirection2;
	private Vector2 shootingDirection3;

	public bool IsLocked
	{
		get { return locked; }
		set { locked = value; }
	}

	private void Awake()
	{
		//Cacher necessary components
		anim = GetComponent<Animator>();
		launcher = GetComponent<Launcher>();

		//Set the position of the player at x = ScreenWidth/4
		transform.position = GetStartingPosition();

		//Calculate the different 3 directions the player can shoot
		CalculateShootingDirections();
	}

	private Vector2 GetStartingPosition()
	{
		return new Vector2(ScreenHelper.instance.LeftSideOfScreen / 2, transform.position.y);
	}

	private void CalculateShootingDirections()
	{
		//90º
		shootingDirection1 = new Vector2(0, 1);

		//60º
		shootingDirection2 = Quaternion.AngleAxis(60f, Vector3.forward) * Vector3.right;

		//30º
		shootingDirection3 = Quaternion.AngleAxis(30f, Vector3.forward) * Vector3.right;
	}

	// Update is called once per frame
	void Update()
    {
		if (locked)
		{
			return;
		}

		handleLeftRightInput();

		handleShootingInput();

		handleUIInput();
		
    }

	void handleLeftRightInput()
	{
		//Handle left and right input
		if (Input.GetButtonDown("Left"))
		{
			//Aim left
			anim.SetBool("AimingLeft", true);
		}
		if (Input.GetButtonUp("Left"))
		{
			//Stop aiming left
			anim.SetBool("AimingLeft", false);
		}
		if (Input.GetButtonDown("Right"))
		{
			//Aim right
			anim.SetBool("AimingRight", true);
		}
		if (Input.GetButtonUp("Right"))
		{
			//Stop aiming right
			anim.SetBool("AimingRight", false);
		}
	}

	void handleShootingInput()
	{
		//Handle shooting input
		if (Input.GetButtonDown("Shoot"))
		{
			//If the player is aiming left, shoot from the launcher to 90º
			if (anim.GetBool("AimingLeft"))
			{
				launcher.Shoot(shootingDirection1);
			}
			//If the player is aiming right, shoot from the launcher to 30º
			else if (anim.GetBool("AimingRight"))
			{
				launcher.Shoot(shootingDirection3);
			}
			//If the player is aiming center, shoot from the launcher to 60º
			else
			{
				launcher.Shoot(shootingDirection2);
			}
		}
	}

	void handleUIInput()
	{
		//Handle Escape button input
		if (Input.GetButtonDown("Cancel"))
		{
			if (!GameManager.instance.IsGamePaused)
			{
				GameManager.instance.PauseGame();
			}
			else
			{
				GameManager.instance.ResumeGame();
			}
		}
	}
}
