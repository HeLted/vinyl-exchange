import React, { Component } from "react";
import AddReleaseComponent from "./AddReleaseComponent";
import uuidv4 from "../../functions/guidGenerator";
import { Url, Controllers } from "./../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../contexts/NotificationContext";

class AddReleaseContainer extends Component {
  constructor() {
    super();
    this.state = {
      formSessionId: uuidv4(),
      artistInput: "d",
      titleInput: "d",
      genreSelectInput: "d",
      styleMultiSelectInput: [],
      formatInput: "d",
      yearInput: "0",
      labelInput: "d",
      genres: [],
      styles: []
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    axios
      .get(
        Url.api +
          Controllers.genres.name +
          Controllers.genres.actions.getAllGenres
      )
      .then(response => {
        this.setState({ genres: response.data });
      })
      .catch(error => {
        this.context.handleServerNotification(error.response);
      });
  }

  componentDidUpdate(prevProps, prevState) {
    if (
      prevState.genreSelectInput !== this.state.genreSelectInput &&
      this.state.genreSelectInput !== "not selected"
    ) {
      axios
        .get(
          Url.api +
            Controllers.styles.name +
            Controllers.styles.actions.getAllStylesForGenre +
            `?genreId=${this.state.genreSelectInput}`
        )
        .then(response => {
          const styles = response.data.map(style => {
            return { value: style.id, label: style.name };
          });

          this.setState({ styles: styles });
        })
        .catch(error => {
          this.context.handleServerNotification(error.response);
        });
    }
  }

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnChangeMultiSelect = value => {
    if (value == null) {
      this.setState({ styleMultiSelectInput: [] });
    } else {
      this.setState({ styleMultiSelectInput: value });
    }
  };

  handleOnSubmit = event => {
    event.preventDefault();
    event.stopPropagation();

    const submitFormObj = {
      formSessionId: this.state.formSessionId,
      artist: this.state.artistInput,
      title: this.state.titleInput,
      styleIds: this.state.styleMultiSelectInput.map(styleObj => {
        return styleObj.value;
      }),
      format: this.state.formatInput,
      year: this.state.yearInput,
      label: this.state.labelInput
    };

    const self = this;
    axios
      .post(
        Url.api +
          Controllers.releases.name +
          Controllers.releases.actions.create,
        submitFormObj
      )
      .then(response => {
        self.context.handleServerNotification(response);
      })
      .catch(error => {
        self.context.handleServerNotification(error.response);
      });
  };

  componentWillUnmount() {
    this.context.handleRemoveAllNotifications();

    axios
      .post(
        Url.api +
          Controllers.files.name +
          Controllers.files.actions.deleteAll +
          `?formSessionId=${this.state.formSessionId}`
      )
      .then(function(response) {
        console.log(response);
      })
      .catch(error => {
        this.context.handleServerNotification(error.response);
      });
  }

  render() {
    return (
      <AddReleaseComponent
        onChange={this.handleOnChange}
        onChangeMultiSelect={this.handleOnChangeMultiSelect}
        onSubmit={this.handleOnSubmit}
        state={this.state}
      />
    );
  }
}

export default AddReleaseContainer;
