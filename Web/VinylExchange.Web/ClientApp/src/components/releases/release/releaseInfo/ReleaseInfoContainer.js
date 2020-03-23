import React, { Component } from "react";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import ReleaseInfoComponent from "./ReleaseInfoComponent";
import axios from "axios";
import { NotificationContext } from "./../../../../contexts/NotificationContext";

class ReleaseInfoContainer extends Component {
  constructor() {
    super();
    this.state = {
      release: { artist: "", title: "", label: "", year: "", format: "" },
      isLoading: false
    };
  }
  static contextType = NotificationContext;

  componentDidMount() {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.releases.name +
          Url.slash +
          this.props.data.releaseId
      )
      .then(response => {
        this.setState({ release: response.data, isLoading: false });
        this.context.handleAppNotification("Loaded release data");
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to load release!"
        );
      });
  }

  render() {
    return (
      <ReleaseInfoComponent
        data={{ release: this.state.release, isLoading: this.state.isLoading }}
      />
    );
  }
}

export default ReleaseInfoContainer;
