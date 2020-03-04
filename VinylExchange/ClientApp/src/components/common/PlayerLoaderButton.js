import React, { Component } from "react";
import { PlayerContext } from "../../contexts/PlayerContext";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeadphones, faEject } from "@fortawesome/free-solid-svg-icons";

class PlayerLoaderButton extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isLoadedToPlayer: false
    };
  }

  static contextType = PlayerContext;

  componentDidMount() {

    this.setState({
      isLoadedToPlayer: this.context.isReleaseLoaded(this.props.data.releaseId)
    });
  }

  handleLoadReleaseToPlayer = (releaseId, event) => {
    event.stopPropagation();
    
    if (!this.context.isReleaseLoaded(releaseId)) {
      this.context.handleLoadRelease(
        releaseId,
        this.handleEjectReleaseFromPlayer
      );
      this.setState({ isLoadedToPlayer: true });
    }
  };

  handleEjectReleaseFromPlayer = (releaseId, event) => {
    if (event != undefined) {
      event.stopPropagation();
    }

    if (this.context.isReleaseLoaded(releaseId)) {
      this.context.handleEjectRelease(releaseId);
      this.setState({ isLoadedToPlayer: false });
    }
  };

  render() {
    const loadToPlayerControlls = !this.state.isLoadedToPlayer ? (
      <button
        className="btn-spr btn btn-outline-dark"
        onClick={event =>
          this.handleLoadReleaseToPlayer(this.props.data.releaseId, event)
        }
        type="button"
      >
        <FontAwesomeIcon icon={faHeadphones} />
      </button>
    ) : (
      <button
        className="btn-spr btn btn-outline-warning"
        onClick={event =>
          this.handleEjectReleaseFromPlayer(this.props.data.releaseId, event)
        }
        type="button"
      >
        <FontAwesomeIcon icon={faEject} />
      </button>
    );

    return loadToPlayerControlls;
  }
}

export default PlayerLoaderButton;
