﻿using Victoria.Lavalink.Enums;
using Victoria.Lavalink.Responses.WebSocket;
using PlayerState = Victoria.Common.Enums.PlayerState;

namespace Victoria.Lavalink.EventArgs
{
    /// <summary>
    ///     Information about track that ended.
    /// </summary>
    public readonly struct TrackEndedEventArgs
    {
        /// <summary>
        ///     Player for which this event fired.
        /// </summary>
        public LavaPlayer Player { get; }

        /// <summary>
        ///     Track sent by Lavalink.
        /// </summary>
        public LavaTrack Track { get; }

        /// <summary>
        ///     Reason for track ending.
        /// </summary>
        public TrackEndReason Reason { get; }

        internal TrackEndedEventArgs(LavaPlayer player, TrackEndEvent endEvent)
        {
            Player = player;
            Track = endEvent.Track;
            Reason = endEvent.Reason;

            if (endEvent.Reason == TrackEndReason.Replaced)
                return;

            player.UpdatePlayer(x =>
            {
                x.PlayerState = PlayerState.Stopped;
                x.Track = default;
            });
        }
    }
}