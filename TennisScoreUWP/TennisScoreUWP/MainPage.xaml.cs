using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TennisScoreUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        Player playerA = new Player();
        Player playerB = new Player();
        
        Dictionary<int, string> correspondingCall = new Dictionary<int, string>() {
            {0, "Love"},
            {1, "15"},
            {2, "30"},
            {3, "40"},
            {4, "Game"},
            {5, "All" },
            {6, "Deuce" },
            {7, "Advantage"}
        };
        
        public void PlayerAClick(object sender, RoutedEventArgs e)
        {
            playerA.Name = "Player A";
            playerB.Name = "Player B";

            AddScore(playerA);
            Score(playerA, playerB);
            var winner = CheckForWinner(playerA, playerB);
            if (winner != null)
            {
                Winner.Text = "The winner is: " + winner.Name;
                Aclick.IsEnabled = false;
                Bclick.IsEnabled = false;
            }
        }

        public void PlayerBClick(object sender, RoutedEventArgs e)
        {
            playerA.Name = "Player A";
            playerB.Name = "Player B";

            AddScore(playerB);
            Score(playerA, playerB);
            var winner = CheckForWinner(playerA, playerB);
            if (winner != null)
            {
                Winner.Text = "The winner is: " + winner.Name;
            }
        }

        public Player AddScore(Player player)
        {
            player.GameScore += 1;
            return player;
        }

        public void Score(Player playerA, Player playerB )
        {
             // If both scorde 3 points or more (deuce)
            if (playerA.GameScore == playerB.GameScore && playerA.GameScore > 2)
            {
                ScorePanelPlayerA.Text = correspondingCall[6];
                ScorePanelPlayerB.Text = correspondingCall[6];
            }

            // Deuce/advantage situation
            else if (playerA.GameScore > 2 && playerB.GameScore > 2)
            {
                if (playerB.GameScore - playerA.GameScore == 1)
                {
                    ScorePanelPlayerA.Text = correspondingCall[6];
                    ScorePanelPlayerB.Text = correspondingCall[7];
                }
                if (playerA.GameScore - playerB.GameScore == 1)
                {
                    ScorePanelPlayerA.Text = correspondingCall[7];
                    ScorePanelPlayerB.Text = correspondingCall[6];
                }
            }
            
            // If both won one or two points(15 - all, 30 - all)
            else if (playerA.GameScore == playerB.GameScore && playerA.GameScore <3)
            {
                ScorePanelPlayerA.Text = correspondingCall[playerA.GameScore]; ;
                ScorePanelPlayerB.Text = correspondingCall[5];
            }

            // if both players are at their first 4 points
            else if (playerA.GameScore < 5 && playerB.GameScore < 5)
            {
                var correspondingCallPlayerA = correspondingCall[playerA.GameScore];
                var correspondingCallPlayerB = correspondingCall[playerB.GameScore];
                ScorePanelPlayerA.Text = correspondingCallPlayerA;
                ScorePanelPlayerB.Text = correspondingCallPlayerB;
            }
           
            // if one player needs to score one point more for the game to end 
            else if (playerA.GameScore < 5 || playerB.GameScore < 5)
            {
                if (playerA.GameScore < 5)
                {
                    var correspondingCallPlayerA = correspondingCall[playerA.GameScore];
                    ScorePanelPlayerA.Text = correspondingCallPlayerA;
                }
                else
                {
                    var correspondingCallPlayerB = correspondingCall[playerB.GameScore];
                    ScorePanelPlayerB.Text = correspondingCallPlayerB;
                }
            }          
        }
   
        public Player CheckForWinner(Player playerA, Player playerB)
        {
            if (playerA.GameScore > 3 && playerB.GameScore >1 && playerA.GameScore - playerB.GameScore > 1)
            {
                return playerA;
            }
            else if (playerB.GameScore > 3 && playerA.GameScore >1 && playerB.GameScore - playerA.GameScore > 1)
            {
                return playerB;
            }
            return null;           
        }
    }
}
