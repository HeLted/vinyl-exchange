import React from "react";
import "./PlayerCustom.css";

export default class Player extends React.Component {
  constructor() {
    super();
    this.state = {
      isHidden: true
    };
  }

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
              <li>
                <div className="sm2-row">
                  <div className="sm2-col sm2-wide">
                    <a href="http://freshly-ground.com/data/audio/sm2/SonReal%20-%20LA%20%28Prod%20Chin%20Injetti%29.mp3">
                      <b>SonReal</b> - LA
                      <span className="Nameabel">Explicit</span>
                    </a>
                  </div>
                  <div className="sm2-col">
                    <a
                      href="http://freshly-ground.com/data/audio/sm2/SonReal%20-%20LA%20%28Prod%20Chin%20Injetti%29.mp3"
                      target="_blank"
                      title='Download "LA"'
                      className="sm2-icon sm2-music sm2-exclude"
                    >
                      Download this track
                    </a>
                  </div>
                </div>
              </li>

              <li>
                <a href="https://vocaroo.com/embed/gU8ZpJ3p7y4">
                  <b>Aphex Twin</b> - Sam's Car{" "}
                  <span className="label">Explicit</span>
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/sm2/SonReal%20-%20People%20Asking.mp3">
                  <b>SonReal</b> - People Asking{" "}
                  <span className="label">Explicit</span>
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/sm2/SonReal%20-%20Already%20There%20Remix%20ft.%20Rich%20Kidd%2C%20Saukrates.mp3">
                  <b>SonReal</b> - Already There Remix ft. Rich Kidd, Saukrates{" "}
                  <span className="label">Explicit</span>
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/sm2/The%20Fugitives%20-%20Graffiti%20Sex.mp3">
                  <b>The Fugitives</b> - Graffiti Sex
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/sm2/Adrian%20Glynn%20-%20Seven%20Or%20Eight%20Days.mp3">
                  <b>Adrian Glynn</b> - Seven Or Eight Days
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/sm2/SonReal%20-%20I%20Tried.mp3">
                  <b>SonReal</b> - I Tried
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/mpc/20060826%20-%20Armstrong.mp3">
                  Armstrong Beat
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/mpc/20090119%20-%20Untitled%20Groove.mp3">
                  Untitled Groove
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/sm2/birds-in-kauai-128kbps-aac-lc.mp4">
                  Birds In Kaua'i (AAC)
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/sm2/20130320%20-%20Po%27ipu%20Beach%20Waves.ogg">
                  Po'ipu Beach Waves (OGG)
                </a>
              </li>
              <li>
                <a href="http://freshly-ground.com/data/audio/sm2/bottle-pop.wav">
                  A corked beer bottle (WAV)
                </a>
              </li>
              <li>
                <a href="../../demo/_mp3/rain.mp3">Rain</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    );
  }
}
