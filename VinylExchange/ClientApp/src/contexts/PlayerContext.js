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

  handleLoadReleaseTracks = (releaseId, releaseName) => {
    let self = this;
    axios
      .get(`/api/releasetracks/getalltracksforrelease?releaseId=${releaseId}`)
      .then(function(response) {
        let fileArray = response.data;
       
        self.setState(prevState=> {
          
          let newReleasesArr = prevState.releases
          newReleasesArr.push({
            id: releaseId,
            name: releaseName,
            tracks: fileArray.map(file => {
              return {
                id: file.id,
                path: file.path + file.fileName,
                name:atob(file.fileName.split("@---@")[1].split(".")[0])
              };
            })
          })

          return {releases:newReleasesArr}
        });

        console.log(self.state.releases);

      })
      .catch(function(error) {
        console.log(error.response);
      });
  };

  render() {
    return (
      <PlayerContext.Provider
        value={{
          ...this.state,
          handleLoadReleaseTracks: this.handleLoadReleaseTracks
        }}
      >
        {this.props.children}
      </PlayerContext.Provider>
    );
  }
}
