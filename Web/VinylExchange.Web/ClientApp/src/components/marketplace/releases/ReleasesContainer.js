import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { css } from "@emotion/core";
import { Url, Controllers, Queries } from "./../../../constants/UrlConstants";
import ReleasesComponent from "./ReleasesComponent";
import PulseLoader from "react-spinners/PulseLoader";
import axios from "axios";
import { NotificationContext } from "./../../../contexts/NotificationContext";
import qsArrayStringify from "./../../../functions/qsArrayStringify";

class ReleasesContainer extends Component {
  constructor() {
    super();
    this.state = {
      releases: [],
      requestURL: "",
      isLoadMoreReleasesLoading: false,
      isThereMoreReleasesToLoad: true,
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.handleLoadReleases();
  }

  componentWillReceiveProps(nextProps) {
    const nextPropsSearchValue =
      nextProps.data.searchValue == undefined ? "" : nextProps.data.searchValue;

    if (
      nextPropsSearchValue !== this.props.data.searchValue ||
      qsArrayStringify(this.props.data.filterStyleIds) !==
        qsArrayStringify(nextProps.data.filterStyleIds)
    ) {
      this.setState({ isThereMoreReleasesToLoad: true });
      this.handleLoadReleases(true);
    }
  }

  handleLoadReleases = (shouldUnloadReleases) => {
    this.setState({
      isLoadMoreReleasesLoading: true,
    });

    clearTimeout(this.timer);
    this.timer = setTimeout(() => {
      const qsStringifiedFilterStyleIds = qsArrayStringify(
        this.props.data.filterStyleIds,
        Queries.styleIds
      );

      const releasesToSkip = shouldUnloadReleases
        ? 0
        : this.state.releases.length;

      const requestURL =
        Url.api +
        Controllers.releases.name +
        Controllers.releases.actions.getReleases +
        Url.queryStart +
        Queries.searchTerm +
        Url.equal +
        this.props.data.searchValue +
        Url.and +
        Queries.filterGenreId +
        Url.equal +
        this.props.data.filterGenreId +
        Url.and +
        Queries.releasesToSkip +
        Url.equal +
        `${releasesToSkip}` +
        `${qsStringifiedFilterStyleIds !== "" ? Url.and : ""}` +
        qsStringifiedFilterStyleIds;

      axios
        .get(requestURL)

        .then((response) => {
          if (
            JSON.stringify(this.state.releases) !==
            JSON.stringify(response.data)
          ) {
            return response.data;
          }

          throw "State not updated!!!";
        })
        .then((data) => {
          if (data.length === 0) {
            if (shouldUnloadReleases) {
              this.setState({
                releases: [],
                isLoadMoreReleasesLoading: false,
                isThereMoreReleasesToLoad: false,
              });
            } else {
              this.setState({
                isLoadMoreReleasesLoading: false,
                isThereMoreReleasesToLoad: false,
              });
            }
          } else {
            if (this.state.isThereMoreReleasesToLoad) {
              this.context.handleAppNotification("Loading more releases", 5);
              this.setState((prevState) => {
                const updatedReleases = shouldUnloadReleases
                  ? []
                  : prevState.releases;
                data.forEach((release) => {
                  updatedReleases.push(release);
                });

                return {
                  releases: updatedReleases,
                  isLoadMoreReleasesLoading: false,
                  isThereMoreReleasesToLoad: true,
                };
              });
            }
          }
        })
        .catch((error) => {
          if (error !== "State not updated!!!") {
            this.context.handleServerNotification(error.response);
          }
          this.setState({
            isLoadMoreReleasesLoading: false,
          });
        });
    }, 1000);
  };

  handleRedirectToRelease = (releaseId) => {
    this.props.history.push(`/Releases/${releaseId}`, { releaseId: releaseId });
  };

  render() {
    const renderComponent = this.state.isLoading ? (
      <PulseLoader size={15} color={"lime"} loading={this.state.isLoading} />
    ) : (
      <ReleasesComponent
        data={{
          releases: this.state.releases,
          isLoadMoreReleasesLoading: this.state.isLoadMoreReleasesLoading,
          isThereMoreReleasesToLoad: this.state.isThereMoreReleasesToLoad,
        }}
        functions={{
          handleLoadReleases: this.handleLoadReleases,
          handleRedirectToRelease: this.handleRedirectToRelease,
        }}
        playerContextFunctions={{
          handleLoadReleaseToPlayer: this.handleLoadReleaseToPlayer,
          handleEjectReleaseFromPlayer: this.handleEjectReleaseFromPlayer,
        }}
      />
    );
    return renderComponent;
  }
}

function ReleaseContainerWrapper(props) {
  return <ReleasesContainer {...props} />;
}

export default withRouter(ReleaseContainerWrapper);
