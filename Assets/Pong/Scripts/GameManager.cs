using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pong.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Ball ball;  
        [SerializeField] private TextMeshProUGUI playerScoreText; // UI Text for player score
        [SerializeField] private TextMeshProUGUI computerScoreText; // UI Text for computer score
        [SerializeField] private TextMeshProUGUI gameWinText; // UI Text for computer score
        [SerializeField] private float score2Win = 5f; // The score needed to win the game
        private float _playerScore;
        private float _computerScore;

        [Header("UI Elements")]
        [SerializeField] private Button startButton; // Drag your Start Button here in the Inspector
        [SerializeField] private Image gameWinTextBackground; // UI Text for computer score
    
        [Header("Game State")]
        [SerializeField] private bool isGameActive; // A simple flag to track the game state

        private void Start()
        {
            // Example: Initialize the game state
            isGameActive = false;
            startButton.gameObject.SetActive(true); // Hide the start button
        }
    
        private void Update()
        {
            // Example: Press 'R' to stop/reset the game for testing purposes
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopGame();
            }
        }
    
        private void ResetGame()
        {
            // Reset the scores
            _playerScore = 0;
            _computerScore = 0;

            // Reset the UI
            playerScoreText.text = _playerScore.ToString(CultureInfo.InvariantCulture);
            computerScoreText.text = _computerScore.ToString(CultureInfo.InvariantCulture);
            gameWinText.text = "";
        
            // Reset the ball
            ball.Restart();
        }

        // This method will be called when the Start Button is clicked
        public void StartGame()
        {
            if (!isGameActive) // Prevent starting multiple times if already active
            {
                isGameActive = true;
                startButton.gameObject.SetActive(false); // Hide the start button
                gameWinTextBackground.gameObject.SetActive(false); // Hide the game win text
            
                ResetGame(); // Reset the game state;
                Utility.Log("Game Started!");
            }
        }
    
        // This method will be called when the game needs to stop/reset
        private void StopGame()
        {
            if (isGameActive)
            {
                isGameActive = false;
                startButton.gameObject.SetActive(true); // Show the start button again
                gameWinTextBackground.gameObject.SetActive(true); // Show the game win text
                // Optionally, update other UI elements here
                Utility.Log("Game Stopped!");
                // Add your game-stopping/resetting logic here (e.g., reset player position, clear scores)
                ball.SetStartPosition();
            }
        }

        public void AddPlayerScore()
        {
            _playerScore++;
            playerScoreText.text = _playerScore.ToString(CultureInfo.InvariantCulture);
            StopIfWin("Player");
        }

        public void AddComputerScore()
        {
            _computerScore++;
            computerScoreText.text = _computerScore.ToString(CultureInfo.InvariantCulture);
            StopIfWin("Computer");
        }
    
        private void StopIfWin(string winner)
        {
            if (CheckForWin(winner))
                StopGame();
            else
                ball.Restart();
        }

        private bool CheckForWin(string winner)
        {
            if (_playerScore >= score2Win || _computerScore >= score2Win)
            {
                // Player wins
                Utility.Log($"{winner} wins!");
                gameWinText.text = $"{winner} wins!";
                return true;
            }
        
            // Continue the game
            Utility.Log($"Current Score - Player: {_playerScore}, Computer: {_computerScore}");
            return false;
        }
    }
}
