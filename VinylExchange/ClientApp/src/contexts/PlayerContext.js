import React, { createContext } from "react";

export const PlayerContext = createContext();

export default class PlayerContextProvider extends React.Component {
  


  
  handleLoadReleaseTracks = (releaseId,releaseName) => {
    
      

  }
  
    render() {
    return (
      <NotificationContext.Provider
        value={{
          ...this.state,
          handleLoadReleaseTracks: this.handleLoadReleaseTracks
        }}
      >
        {this.props.children}
      </NotificationContext.Provider>
    );
  }
}
