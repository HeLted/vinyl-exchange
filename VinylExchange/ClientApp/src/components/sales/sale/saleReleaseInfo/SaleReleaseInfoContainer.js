import React, { Component } from "react";
import SaleReleaseInfoComponent from "./SaleReleaseInfoComponent";
import axios from "axios";
import { Url, Controllers } from "./../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../contexts/NotificationContext";


class SaleReleaseInfoContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      releaseId: "",
      artist: "",
      title: "",
      coverArt: "",
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
        const data = response.data;
        this.setState({
          releaseId: this.props.data.releaseId,
          artist: data.artist,
          title: data.title,
          coverArt:
            Url.mediaStorage + data.coverArt.path + data.coverArt.fileName,
          isLoading: false
        });
        this.context.handleAppNotification("Loaded release data", 5);
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "There was an error while loading release!"
        );
      });
  }

  render() {
    return (
      <SaleReleaseInfoComponent
        data={{
          releaseId: this.state.releaseId,
          artist: this.state.artist,
          title: this.state.title,
          coverArt:this.state.coverArt,
          isLoading:this.state.isLoading
        }}
      />
    );
  }
}

export default SaleReleaseInfoContainer;
