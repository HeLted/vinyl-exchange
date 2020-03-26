import React, { Component } from "react";

class PlayerControlls extends Component {
  render() {
    return (
      <div className="container-fluid custom-player-controls ">
        <div className="row">
        <div className="sm2-inline-texture"></div>
            <div className="sm2-inline-gradient"></div>
          <div className="custom-player-controls bd sm2-main-controls">
            <div className="col-lg-10 col-sm-8 col-xs-2 p-0 m-0">
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

              <div className="custom-player-status sm2-inline-element sm2-inline-status">
                <div className="sm2-playlist">
                  <div className="sm2-playlist-target">
                    <noscript>
                      <p>JavaScript is required.</p>
                    </noscript>
                  </div>
                </div>

                <div className="row">
                  <div className="custom-player-progressbar sm2-progress">
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
              </div>
            </div>

            <div className="col-lg-2 col-sm-4 col-xs-10 p-0 m-0">
             
            <div className="sm2-inline-texture"></div>
            <div className="sm2-inline-gradient"></div>

            <div className="sm2-inline-element sm2-button-element sm2-menu">
                <div className="sm2-button-bd">
                  <a
                    className="sm2-inline-button sm2-icon-menu"
                    onClick={this.props.functions.handleOnHideShowPlayer}
                  >
                    menu
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
                <div className="sm2-button-bd" style={{opacity:"0.5"}}>
                  <a
                    href="#repeat"
                    title="Repeat playlist"
                    className="sm2-inline-button sm2-icon-repeat"
                  >
                    &infin; repeat
                  </a>
                </div>
              </div>


            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default PlayerControlls;
