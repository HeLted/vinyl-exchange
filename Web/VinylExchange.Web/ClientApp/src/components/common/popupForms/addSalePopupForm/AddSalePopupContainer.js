import React, { Component } from "react";
import AddSalePopupComponent from "./AddSalePopupComponent";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../contexts/NotificationContext";
import axios from "axios";
import getAntiForgeryAxiosConfig from "./../../../../functions/getAntiForgeryAxiosConfig";

class AddSalePopupContainer extends Component {
  constructor() {
    super();
    this.state = {
      releaseId: "",
      collectionItemId: "",
      descriptionInput: "",
      vinylGradeInput: "0",
      sleeveGradeInput: "0",
      priceInput: 0
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.setState({
      releaseId: this.props.data.releaseId,
      collectionItemId: this.props.data.collectionItemId
    });
  }

  handleLoadColletionItemData = () => {
    if(this.state.collectionItemId != undefined){
      axios
      .get(
        Url.api +
          Controllers.collections.name +
          Url.slash +
          this.state.collectionItemId
      )
      .then(response => {
        const data = response.data;
        this.setState({
          descriptionInput: data.description,
          vinylGradeInput: data.vinylGrade.toString(),
          sleeveGradeInput: data.sleeveGrade.toString(),
          priceInput: 0 // on every add sale modal expand setting price to 0
        });
        this.context.handleAppNotification(
          "Loaded collection item data into form",
          5
        );
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to load collection item info"
        );
      });
    }
  };

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = event => {
    event.preventDefault();

    const submitFormObj = {
      releaseId: this.state.releaseId,
      description: this.state.descriptionInput,
      vinylGrade: this.state.vinylGradeInput,
      sleeveGrade: this.state.sleeveGradeInput,
      price: this.state.priceInput
    };

    axios
      .post(
        Url.api + Controllers.sales.name + Url.slash,
        submitFormObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification("Sucessfully created sale", 4);
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to create sale!"
        );
      });
  };

  render() {
    return (
      <AddSalePopupComponent
        data={{
          collectionItemId: this.state.collectionItemId,
          descriptionInput: this.state.descriptionInput,
          vinylGradeInput: this.state.vinylGradeInput,
          sleeveGradeInput: this.state.sleeveGradeInput,
          priceInput: this.state.priceInput
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit,
          handleLoadColletionItemData: this.handleLoadColletionItemData
        }}
      />
    );
  }
}

export default AddSalePopupContainer;
