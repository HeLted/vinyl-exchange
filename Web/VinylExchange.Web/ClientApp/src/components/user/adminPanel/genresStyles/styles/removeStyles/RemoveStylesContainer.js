import React, { Component } from "react";
import RemoveStylesComponent from "./RemoveStylesComponent";
import axios from "axios";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../../../contexts/NotificationContext";
import getAntiForgeryAxiosConfig from "../../../../../../functions/getAntiForgeryAxiosConfig";

class RemoveStylesContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      styles: [],
      genreSelectInput: ""
    };
  }

  static contextType = NotificationContext;

  loadStyles = genreId => {
    axios
      .get(
        Url.api +
          Controllers.styles.name +
          Controllers.styles.actions.getAllStylesForGenre +
          Url.queryStart +
          Queries.genreId +
          Url.equal +
          genreId
      )
      .then(response => {
        this.context.handleAppNotification("Loaded styles for genre", 5);
        this.setState({ styles: response.data });
      })
      .catch(error => {
        this.handleServerNotification(
          error.response,
          "Failed to load styles for genre!"
        );
      });
  };

  handleOnDeleteStyle = styleId => {
    axios
      .delete(
        Url.api + Controllers.styles.name + Url.slash + styleId,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification("Sucessfully deleted style", 4);
        this.setState(prevState => {
          const updatedStyles = prevState.styles.filter(styleObj => {
            return styleObj.id !== styleId;
          });

          return { styles: updatedStyles };
        });
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to delete style!"
        );
      });
  };

  handleOnChangeGenreSelect = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });

    this.loadStyles(value);
  };

  render() {
    return (
      <RemoveStylesComponent
        data={{
          genres: this.props.data.genres,
          styles: this.state.styles,
          genreSelectInput: this.state.genreSelectInput
        }}
        functions={{
          handleOnDeleteStyle: this.handleOnDeleteStyle,
          handleOnChangeGenreSelect: this.handleOnChangeGenreSelect
        }}
      />
    );
  }
}

export default RemoveStylesContainer;
