import React from "react";
import "./PlayerCustom.css";
import PlayerRelease from "./PlayerRelease";
import { PlayerContext } from "./../../contexts/PlayerContext";

export default class Player extends React.Component {
  constructor() {
    super();
    this.state = {
      isHidden: true
    };
  }

  static contextType = PlayerContext;

  handleOnToggle = event => {
    event.preventDefault();
    this.setState(prevState => {
      return {
        isHidden: prevState.isHidden ? false : true
      };
    });
  };

  render() {
    const className = this.state.isHidden ? "hidden" : "hidden open";

    return (
      <div className="player-wrapper container-fluid">
        <div className="player-on-top sm2-bar-ui full-width fixed">
          <div className="custom-player-controls bd sm2-main-controls">
            <div className="sm2-inline-texture"></div>
            <div className="sm2-inline-gradient"></div>

            <div className="sm2-inline-element sm2-button-element">
              <div className="sm2-button-bd">
                <a
                  href="#play"
                  className="sm2-inline-button sm2-icon-play-pause"
                >
                  Play / pause
                </a>
              </div>
            </div>

            <div className="sm2-inline-element sm2-inline-status">
              <div className="sm2-playlist">
                <div className="sm2-playlist-target">
                  <noscript>
                    <p>JavaScript is required.</p>
                  </noscript>
                </div>
              </div>

              <div className="sm2-progress">
                <div className="sm2-row">
                  <div className="sm2-inline-time">0:00</div>
                  <div className="sm2-progress-bd">
                    <div className="sm2-progress-track">
                      <div className="sm2-progress-bar"></div>
                      <div className="custom-player-progressball sm2-progress-ball">
                        <div className="icon-overlay"></div>
                      </div>
                    </div>
                  </div>
                  <div className="sm2-inline-duration">0:00</div>
                </div>
              </div>
            </div>

            <div className="sm2-inline-element sm2-button-element sm2-volume">
              <div className="sm2-button-bd">
                <span className="sm2-inline-button sm2-volume-control volume-shade"></span>
                <a
                  href="#volume"
                  className="sm2-inline-button sm2-volume-control"
                >
                  volume
                </a>
              </div>
            </div>

            <div className="sm2-inline-element sm2-button-element">
              <div className="sm2-button-bd">
                <a
                  href="#prev"
                  title="Previous"
                  className="sm2-inline-button sm2-icon-previous"
                >
                  &lt; previous
                </a>
              </div>
            </div>

            <div className="sm2-inline-element sm2-button-element">
              <div className="sm2-button-bd">
                <a
                  href="#next"
                  title="Next"
                  className="sm2-inline-button sm2-icon-next"
                >
                  &gt; next
                </a>
              </div>
            </div>

            <div className="sm2-inline-element sm2-button-element">
              <div className="sm2-button-bd">
                <a
                  href="#repeat"
                  title="Repeat playlist"
                  className="sm2-inline-button sm2-icon-repeat"
                >
                  &infin; repeat
                </a>
              </div>
            </div>

            <div className="sm2-inline-element sm2-button-element disabled">
              <div className="sm2-button-bd">
                <a
                  href="#shuffle"
                  title="Shuffle"
                  className="sm2-inline-button sm2-icon-shuffle"
                >
                  shuffle
                </a>
              </div>
            </div>

            <div className="sm2-inline-element sm2-button-element sm2-menu">
              <div className="sm2-button-bd">
                <a
                  className="sm2-inline-button sm2-icon-menu"
                  onClick={this.handleOnToggle}
                >
                  menu
                </a>
              </div>
            </div>
          </div>

          <div className="bd sm2-playlist-drawer sm2-element">
            <div className="sm2-inline-texture">
              <div className="sm2-box-shadow"></div>
            </div>
          </div>

          <div
            className={
              className + " custom-playlist-wrapper sm2-playlist-wrapper"
            }
          >
            <ul className="sm2-playlist-bd">
              <div className="playlistContainer container-fluid p-0">
                {this.context.releases.map(release => {
                  return (
                    <PlayerRelease
                      handleRemoveReleaseFromPlayer={
                        this.context.handleRemoveReleaseFromPlayer
                      }
                      name={release.name}
                      tracks={release.tracks}
                      image={release.image}
                      key={release.id}
                      releaseId={release.id}
                    />
                  );
                })}
              </div>
            </ul>
          </div>
        </div>
      </div>
    );
  }
}
