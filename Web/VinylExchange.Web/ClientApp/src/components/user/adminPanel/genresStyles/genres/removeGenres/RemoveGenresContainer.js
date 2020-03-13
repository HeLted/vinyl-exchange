import React, { Component } from "react";
import RemoveGenresComponent from "./RemoveGenresComponent";
import axios from "axios";
import {
  Url,
  Controller,
  Controllers
} from "../../../../../../constants/UrlConstants";
import getAntiForgeryAxiosConfig from "../../../../../../functions/getAntiForgeryAxiosConfig";
import {NotificationContext} from "../../../../../../contexts/NotificationContext";

class RemoveGenresContainer extends Component {

  static contextType = NotificationContext;

  handleOnDeleteGenre = genreId => {
    axios
      .delete(
        Url.api + Controllers.genres.name + Url.slash + genreId,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification(
          "Sucessfully deleted genre and its styles",
          4
        );
       
     
       this.props.functions.handleReLoad();
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to delete genre!"
        );
      });
  };

  render() {
    return (
      <RemoveGenresComponent
        data={{ genres: this.props.data.genres }}
        functions={{ handleOnDeleteGenre: this.handleOnDeleteGenre }}
      />
    );
  }
}

export default RemoveGenresContainer;
