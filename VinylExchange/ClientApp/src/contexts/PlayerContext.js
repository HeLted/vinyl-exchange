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

  handleLoadReleaseTracks = (releaseId, releaseName,imagePath,imageName) => {
    
    let found = false;
    for(var i = 0; i < this.state.releases.length; i++) {
        if (this.state.releases[i].id == releaseId) {
            found = true;
            break;
        }
    }

    if(!found){
      let self = this;
      axios
        .get(`/api/releasetracks/getalltracksforrelease?releaseId=${releaseId}`)
        .then(function(response) {
          const fileArray = response.data;
          self.setState(prevState=> {
            
            const updatedReleases = prevState.releases
            updatedReleases.push({
              id: releaseId,
              name: releaseName,
              image: imagePath + imageName,
              tracks: fileArray.map(file => {
                return {
                  id: file.id,
                  path: file.path + file.fileName,
                  name:atob(file.fileName.split("@---@")[1].split(".")[0])
                };
              })
            })
  
            return {releases:updatedReleases}
          });
  
        })
        .catch(function(error) {
          console.log(error.response);
        });
    }
    
    
   
  };

  handleRemoveReleaseFromPlayer = (releaseId) => {
    
    this.setState(prevState=>{
      const updatedReleases = prevState.releases.filter(release=> release.id !== releaseId)   
      
      return ({
        releases:updatedReleases
      })
    })
  }

  render() {
    return (
      <PlayerContext.Provider
        value={{
          ...this.state,
          handleLoadReleaseTracks: this.handleLoadReleaseTracks,
          handleRemoveReleaseFromPlayer:this.handleRemoveReleaseFromPlayer
        }}
      >
        {this.props.children}
      </PlayerContext.Provider>
    );
  }
}
