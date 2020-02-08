import React, { createContext } from "react";
import axios from "axios";
import uuidv4 from "./../guidGenerator";

export const PlayerContext = createContext();

export default class PlayerContextProvider extends React.Component {
  constructor() {
    super();
    this.state = {
      releases: []
    };
  }

  handleLoadRelease = (releaseId, releaseName, imagePath, imageName,ejectReleaseCallback) => {
    let found = false;
    for (var i = 0; i < this.state.releases.length; i++) {
      if (this.state.releases[i].id == releaseId) {
        found = true;
        break;
      }
    }

    if (!found) {
      let self = this;
      axios
        .get(`/api/releasetracks/getalltracksforrelease?releaseId=${releaseId}`)
        .then(function(response) {
          const fileArray = response.data;
          self.setState(prevState => {
            const updatedReleases = prevState.releases;
            updatedReleases.push({
              id: releaseId,
              name: releaseName,
              image: imagePath + imageName,
              tracks: fileArray.map(file => {
                return {
                  id: file.id,
                  path: file.path + file.fileName,
                  name: atob(file.fileName.split("@---@")[1].split(".")[0])
                };
              }),
              ejectReleaseCallback:ejectReleaseCallback
            });

            return { releases: updatedReleases };
          });
        })
        .catch(function(error) {
          console.log(error.response);
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
