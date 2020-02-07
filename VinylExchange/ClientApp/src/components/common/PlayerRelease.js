import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAngleDown,faAngleUp ,faTimes } from "@fortawesome/free-solid-svg-icons";
import PlayerTrack from "./PlayerTrack";

export default class PlyerRelease extends React.Component {
  
  constructor(){
    super();
    this.state = {
      isHidden : true
    }
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

    const trakcsUlDisplay = this.state.isHidden ? "none" : "block"

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
              <a className="releaseAnchor">{this.props.name}</a>
            </div>
          </div>
          <div className="col-1 p-0">
            <button className="btn btn-primary w-50" onClick={this.handleOnToggle} type="button">
              {icon}
            </button>
            <button className="btn btn-danger w-50" onClick={this.handleOnToggle} type="button">
              <FontAwesomeIcon icon={faTimes} />
            </button>
          </div>
        </div>
        <div className="player-row row bg-dark">
          <ul class="list-group" style={{display:trakcsUlDisplay}} active>
            {this.props.tracks.map((track, index) => {
              return (
                <PlayerTrack path={track.path} name={track.name} key={track.id} />
              );
            })}
          </ul>
        </div>
      </li>
    );
  }
}
