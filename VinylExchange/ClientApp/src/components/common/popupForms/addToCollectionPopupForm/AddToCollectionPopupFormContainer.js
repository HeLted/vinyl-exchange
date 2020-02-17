import React, { Component } from "react";
import AddToCollectionPopupFormComponent from "./AddToCollectionPopupFormComponent";
import authService from "./../../../api-authorization/AuthorizeService";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../contexts/NotificationContext";
import axios from "axios";

class AddtoColletionPopupFormContainer extends Component {
  constructor() {
    super();
    this.state = {
      releaseId: "",
      userId: "",
      descriptionInput: "",
      vinylGradeInput: 0,
      sleeveGradeInput: 0,
      isReleaseAlreadyInCollection:false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    const self = this;
    authService.getUser().then(userObject => {
      self.setState({
        releaseId: this.props.data.releaseId,
        userId: userObject.sub
      });
    });
  }

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = event => {
    event.preventDefault();
    const self = this;

    const submitFormObj = {
      vinylGrade: this.state.vinylGradeInput,
      sleeveGrade: this.state.sleeveGradeInput,
      description: this.state.descriptionInput
    };

    axios
      .post(
        Url.api +
          Controllers.collections.name +
          Controllers.collections.actions.add +
          Url.queryStart +
          Queries.releaseId +
          Url.equal +
          this.state.releaseId +
          Url.and +
          Queries.userId +
          Url.equal + 
          this.state.userId,
        submitFormObj
      )
      .then(response => {
        self.context.handleServerNotification(response);
        this.setState({isReleaseAlreadyInCollection:true})
      })
      .catch(error => {
        self.context.handleServerNotification(error.response);
      });
  };

  render() {
    return (
      <AddToCollectionPopupFormComponent
        data={{
          descriptionInput: this.state.descriptionInput,
          vinylGradeInput: this.state.vinylGradeInput,
          sleeveGradeInput: this.state.sleeveGradeInput,
          isReleaseAlreadyInCollection: this.state.isReleaseAlreadyInCollection
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit
        }}
      />
    );
  }
}

export default AddtoColletionPopupFormContainer;
