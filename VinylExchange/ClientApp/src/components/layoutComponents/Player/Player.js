import React from "react";
import PlayerControlls from "./PlayerControlls";
import PlayerRelease from "./PlayerRelease";
import { PlayerContext } from "../../../contexts/PlayerContext";
import "./PlayerCustom.css";

export default class Player extends React.Component {
  constructor() {
    super();
    this.state = {
      isHidden: true
    };
  }

  static contextType = PlayerContext;

  handleOnHideShowPlayer = event => {
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
          
          <PlayerControlls
            functions={{handleOnHideShowPlayer: this.handleOnHideShowPlayer }}
          />

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
                      handleEjectRelease={release.ejectReleaseCallback}
                      artist={release.artist}
                      title={release.title}
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
