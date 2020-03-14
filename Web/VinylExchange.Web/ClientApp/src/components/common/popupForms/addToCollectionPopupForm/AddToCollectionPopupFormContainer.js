import React, { Component } from "react";
import AddToCollectionPopupFormComponent from "./AddToCollectionPopupFormComponent";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../contexts/NotificationContext";
import axios from "axios";
import getAntiForgeryAxiosConfig  from "./../../../../functions/getAntiForgeryAxiosConfig";
import hideModal from "./../../../../functions/hideModal";

class AddtoColletionPopupFormContainer extends Component {
  constructor() {
    super();
    this.state = {
      releaseId: "",
      descriptionInput: "",
      vinylGradeInput: 0,
      sleeveGradeInput: 0,
      isReleaseAlreadyInUserCollection: false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    axios
      .get(
        Url.api +
          Controllers.collections.name +
          Controllers.collections.actions.doesUserCollectionContainRelease +
          Url.queryStart +
          Queries.releaseId +
          Url.equal +
          this.props.data.releaseId
      )
      .then(response => {
        this.context.handleAppNotification("Release is in user collection", 5);
        this.setState({
          releaseId: this.props.data.releaseId,
          isReleaseAlreadyInUserCollection:
            response.data.doesUserCollectionContainRelease
        });
      })
      .catch(error => {
        this.context.handleServerNotification(error.response);
      });
  }

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = event => {
    event.preventDefault();
    event.stopPropagation();
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
          Url.queryStart +
          Queries.releaseId +
          Url.equal +
          this.state.releaseId,
        submitFormObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification(
          "Sucesfully added release to collection",
          4
        );
        this.setState({ isReleaseAlreadyInUserCollection: true });
        hideModal();
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
          isReleaseAlreadyInUserCollection: this.state
            .isReleaseAlreadyInUserCollection
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
