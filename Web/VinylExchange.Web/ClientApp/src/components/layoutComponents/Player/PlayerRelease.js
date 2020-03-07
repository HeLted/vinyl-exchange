import React ,{Component} from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleDown,
  faAngleUp,
  faEject
} from "@fortawesome/free-solid-svg-icons";
import PlayerTrack from "./PlayerTrack";

class PlayerRelease extends Component {
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
    const trakcsUlDisplay = this.state.isHidden ? "none" : "block";

    const icon =
      this.state.isHidden === true ? (
        <FontAwesomeIcon icon={faAngleDown} />
      ) : (
        <FontAwesomeIcon icon={faAngleUp} />
      );

    return (
      <li>
        <div className="player-row row ">
          <div className="col-11 sm2-row p-0">
            <div className="sm2-col sm2-wide">
              <div className="player-row row">
                <div className="col-1 p-0 m-0">
                  <img src={this.props.image} height="38px" width="38px" />
                </div>
                <div className="col-11 p-0">
                  <a className="releaseAnchor">
                    {this.props.artist} - {this.props.title}
                  </a>
                </div>
              </div>
            </div>
          </div>
          <div className="col-1 p-0">
            <button
              className="btn btn-primary w-50"
              onClick={this.handleOnToggle}
              type="button"
            >
              {icon}
            </button>
            <button
              className="btn btn-warning w-50"
              onClick={() =>
                this.props.handleEjectRelease(this.props.releaseId)
              }
              type="button"
            >
              <FontAwesomeIcon icon={faEject} />
            </button>
          </div>
        </div>
        <div className="player-row row bg-dark">
          <div className="col-12 p-0">
            <ul className="list-group" style={{ display: trakcsUlDisplay }}>
              {this.props.tracks.map((track, index) => {
                return (
                  <PlayerTrack
                    path={track.path}
                    name={track.name}
                    key={track.id}
                  />
                );
              })}
            </ul>
          </div>
        </div>
      </li>
    );
  }
}

export default PlayerRelease;