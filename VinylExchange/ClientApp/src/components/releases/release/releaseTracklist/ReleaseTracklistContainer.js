import React, { Component } from "react";
import ReleaseTracklistComponent from "./ReleaseTracklistComponent";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../../../contexts/NotificationContext";

class ReleaseTracklistContainer extends Component {
  constructor() {
    super();
    this.state = {
      tracks: [],
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.releaseTracks.name +
          Controllers.releaseTracks.actions.getAllTracksForRelease +
          Url.slash +
          this.props.data.releaseId
      )
      .then(response => {
        this.setState({ tracks: response.data, isLoading: false });
        this.context.handleAppNotification("Loaded tracklist data");
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to load tracklist data!"
        );
      });
  }

  render() {
    return (
      <ReleaseTracklistComponent
        data={{ tracks: this.state.tracks, isLoading: this.state.isLoading }}
      />
    );
  }
}

export default ReleaseTracklistContainer;
