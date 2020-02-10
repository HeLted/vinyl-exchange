import React from "react";
import AddReleaseComponent from "./AddReleaseComponent";
import uuidv4 from "../../functions/guidGenerator";
import axios from "axios";
import { NotificationContext } from "./../../contexts/NotificationContext";

export default class AddReleaseContainer extends React.Component {
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
    fetch("/api/genres/getallgenres")
      .then(response => response.json())
      .then(data => {
        this.setState({ genres: data });
      });
  }

  componentDidUpdate(prevProps, prevState) {
    if (
      prevState.genreSelectInput !== this.state.genreSelectInput &&
      this.state.genreSelectInput !== "not selected"
    ) {
      fetch(
        `/api/styles/getallstylesforgenre?genreId=${this.state.genreSelectInput}`
      )
        .then(response => response.json())
        .then(data => {
          const styles = data.map(style => {
            return { value: style.id, label: style.name };
          });

          this.setState({ styles: styles });
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

    let self = this;
    axios
      .post("/api/releases/create", submitFormObj)
      .then(function(response) {
        console.log(response)

        self.context.handleServerNotification(response);
      })
      .catch(function(error) {
        console.log(error)
        self.context.handleServerNotification(error.response);
      });
  };

  componentWillUnmount(){

    this.context.handleRemoveAllNotifications();

    axios
      .post(`/api/file/deleteall?formSessionId=${this.state.formSessionId}`)
      .then(function(response) {
        console.log(response)
      })
      .catch(function(error) {
        console.log(error.response)
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
