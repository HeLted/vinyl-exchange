import React, { Component, createContext } from "react";
import axios from "axios";
import { Url, Controllers, Queries } from "./../constants/UrlConstants";
import { NotificationContext } from "./NotificationContext";

class PlayerContextProvider extends Component {
  constructor() {
    super();
    this.state = {
      releases: []
    };
  }

  static contextType = NotificationContext;

  handleLoadRelease = async (releaseId, ejectReleaseCallback) => {
    let found = false;
    for (var i = 0; i < this.state.releases.length; i++) {
      if (this.state.releases[i].id == releaseId) {
        found = true;
        break;
      }
    }

    if (!found) {
      let self = this;

      const releaseDataPromise = await this.getReleasePromise(releaseId);
      const releaseTracksDataPromise = await this.getReleaseTracksPromise(
        releaseId
      );

      Promise.all([
        releaseDataPromise,
        releaseTracksDataPromise
      ]).then(values => {

        this.context.handleAppNotification("Added Release To Player",2);

        const releaseData = values[0].data;
        const releaseTracksData = values[1].data;

        self.setState(prevState => {
          const updatedReleases = prevState.releases;
          updatedReleases.push({
            id: releaseData.id,
            artist: releaseData.artist,
            title: releaseData.title,
            image:
              Url.mediaStorage +
              releaseData.coverArt.path +
              releaseData.coverArt.fileName,
            tracks: releaseTracksData.map(track => {
              return {
                id: track.id,
                path: Url.mediaStorage + track.path + track.fileName,
                name: atob(track.fileName.split("@---@")[1].split(".")[0])
              };
            }),
            ejectReleaseCallback: ejectReleaseCallback
          });

          return { releases: updatedReleases };
        });
      });
    }
  };

  handleEjectRelease = releaseId => {
    this.setState(prevState => {
      const updatedReleases = prevState.releases.filter(
        release => release.id !== releaseId
      );
      return {
        releases: updatedReleases
      };
    });
  };

  isReleaseLoaded = releaseId => {
    return this.state.releases.some(r => r.id === releaseId);
  };

  getReleasePromise = async releaseId => {
    return axios
      .get(Url.api + Controllers.releases.name + Url.slash + releaseId)
      .catch(error => {
        this.context.handleServerNotification(error.response);
        throw "Reject Promise";
      });
  };

  getReleaseTracksPromise = async releaseId => {
    return await axios
      .get(
        Url.api +
          Controllers.releaseTracks.name +
          Controllers.releaseTracks.actions.getAllTracksForRelease +
          Url.queryStart +
          Queries.releaseId +
          Url.equal +
          releaseId
      )
      .catch(error => {
        this.context.handleServerNotification(error.response);
        throw "Reject Promise";
      });
  };

  render() {
    return (
      <PlayerContext.Provider
        value={{
          ...this.state,
          handleLoadRelease: this.handleLoadRelease,
          handleEjectRelease: this.handleEjectRelease,
          handleRemoveReleaseFromPlayer: this.handleRemoveReleaseFromPlayer,
          isReleaseLoaded: this.isReleaseLoaded
        }}
      >
        {this.props.children}
      </PlayerContext.Provider>
    );
  }
}

export const PlayerContext = createContext();
export default PlayerContextProvider;
