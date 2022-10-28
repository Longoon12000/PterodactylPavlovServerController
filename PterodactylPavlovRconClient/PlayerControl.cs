﻿using PterodactylPavlovRconClient.Models;
using PterodactylPavlovRconClient.Properties;
using PterodactylPavlovServerController.Models;
using RestSharp;
using Steam.Models.SteamCommunity;
using System.Diagnostics;

namespace PterodactylPavlovRconClient
{
    public partial class PlayerControl : UserControl
    {
        private readonly RestClient restClient;
        private readonly PterodactylServerModel server;
        private readonly string uniqueId;
        private PlayerModel playerModel;

        private bool disconnected = false;
        public bool Disconnected
        {
            get => disconnected; set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(() => Disconnected = value);
                    return;
                }

                disconnected = value;

                btnKick.Visible = !disconnected;

                if (disconnected)
                {
                    this.pbConnection.Image = Resources.signal_offline;
                    this.pbPlayerState.Image = null;
                }
            }
        }
        public string UniqueId => uniqueId;
        public int Team => playerModel.TeamId;

        private bool banned = false;
        public bool Banned
        {
            get => banned; set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(() => Banned = value);
                    return;
                }

                banned = value;

                pbBanned.Image = banned ? Resources.ban : null;
                btnBan.Text = banned ? "Unban" : "Ban";
            }
        }

        public PlayerControl(RestClient restClient, PterodactylServerModel server, string uniqueId, PlayerModel? playerModel = null)
        {
            InitializeComponent();
            this.restClient = restClient;
            this.server = server;
            this.uniqueId = uniqueId;

            this.playerModel = playerModel ?? new PlayerModel()
            {
                UniqueId = this.UniqueId
            };
            updateValues(this.playerModel);
        }

        private async void PlayerControl_Load(object sender, EventArgs e)
        {
            await RefreshPlayer();
            await RefreshSteamBans();
        }

        public async Task RefreshSteamBans()
        {
            pbLoading.Visible = true;
            RestResponse<PlayerBansModel[]> steamBanResponse = await restClient.ExecuteAsync<PlayerBansModel[]>(new RestRequest($"steam/bans?steamId={uniqueId}"));
            if (!steamBanResponse.IsSuccessful)
            {
                Console.WriteLine($"Could not get steam bans for user {uniqueId}");
                lblVacBans.Text = "Steam unreachable";
                lblGameBans.Text = "Steam unreachable";
                lblDaysSinceLastBan.Text = "Steam unreachable";
                return;
            }

            int gameBans = 0;
            int vacBans = 0;
            int daysSinceLastBan = -1;
            bool currentlyVacBanned = false;

            foreach (PlayerBansModel playerBans in steamBanResponse.Data!)
            {
                if ((playerBans.NumberOfVACBans > 0 || playerBans.NumberOfGameBans > 0 || playerBans.DaysSinceLastBan > 0) && (daysSinceLastBan == -1 || daysSinceLastBan < playerBans.DaysSinceLastBan))
                {
                    daysSinceLastBan = (int)playerBans.DaysSinceLastBan;
                }

                vacBans += (int)playerBans.NumberOfVACBans;
                gameBans += (int)playerBans.NumberOfGameBans;

                if (playerBans.VACBanned)
                {
                    currentlyVacBanned = true;
                }
            }

            lblVacBans.Text = $"VAC: {vacBans}x";
            lblGameBans.Text = $"Games: {gameBans}x";
            lblDaysSinceLastBan.Text = $"Last: {(daysSinceLastBan == -1 ? "never" : $"{daysSinceLastBan} days ago")}";

            if (currentlyVacBanned)
            {
                lblVacBans.ForeColor = Color.Red;
            }
            else if (vacBans > 0)
            {
                lblVacBans.ForeColor = Color.Blue;
            }

            if (gameBans > 0)
            {
                lblGameBans.ForeColor = Color.Red;
            }

            if (daysSinceLastBan != -1)
            {
                lblDaysSinceLastBan.ForeColor = Color.Red;
            }
            pbLoading.Visible = false;
        }

        public async Task RefreshPlayer()
        {
            if (Banned || Disconnected)
            {
                return;
            }

            pbLoading.Visible = true;

            RestResponse<PlayerModel> playerInfoResponse = await restClient.ExecuteAsync<PlayerModel>(new RestRequest($"rcon/player?serverId={server.ServerId}&uniqueId={uniqueId}"));
            if (!playerInfoResponse.IsSuccessful)
            {
                updateValues(null);
            }
            else
            {
                playerModel = playerInfoResponse.Data!;
                updateValues(playerModel);
            }
            pbLoading.Visible = false;
        }

        private void updateValues(PlayerModel? player)
        {
            if (InvokeRequired)
            {
                Invoke(() => updateValues(player));
                return;
            }

            if (player is null)
            {
                this.pbConnection.Image = Resources.signal_unstable;
                this.pbPlayerState.Image = null;
                return;
            }

            this.lblPlayerName.Text = player.PlayerName;
            this.lblKills.Text = player.Kills.ToString();
            this.lblDeaths.Text = player.Deaths.ToString();
            this.lblAssists.Text = player.Assists.ToString();
            this.lblCash.Text = $"${player.Cash}";
            this.lblScore.Text = player.Score.ToString();
            this.lblSteamId.Text = player.UniqueId.ToString();

            this.pbPlayerState.Image = player.Dead ? Resources.skull : Resources.heart;
            this.pbConnection.Image = Resources.signal_online;
        }

        private void btnOpenProfile_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo($"http://steamcommunity.com/profiles/{uniqueId}/") { UseShellExecute = true });
        }

        private async void btnKick_Click(object sender, EventArgs e)
        {
            RestResponse kickResponse = await restClient.ExecuteAsync(new RestRequest($"rcon/kick?serverId={server.ServerId}&uniqueId={uniqueId}", Method.Post));
            if (!kickResponse.IsSuccessful)
            {
                MessageBox.Show($"Kick failed: {kickResponse.Content}");
                return;
            }
        }

        private async void btnBan_Click(object sender, EventArgs e)
        {
            if (Banned)
            {
                RestResponse unbanResponse = await restClient.ExecuteAsync(new RestRequest($"rcon/unban?serverId={server.ServerId}&uniqueId={uniqueId}", Method.Post));
                if (!unbanResponse.IsSuccessful)
                {
                    MessageBox.Show($"Unban failed: {unbanResponse.StatusCode} {unbanResponse.Content}");
                    return;
                }
                Banned = false;
            }
            else
            {
                RestResponse banResponse = await restClient.ExecuteAsync(new RestRequest($"rcon/ban?serverId={server.ServerId}&uniqueId={uniqueId}", Method.Post));
                if (!banResponse.IsSuccessful)
                {
                    MessageBox.Show($"Ban failed: {banResponse.StatusCode} {banResponse.Content}");
                    return;
                }
                Banned = true;
            }
        }
    }
}