import React, { Component } from "react";
import EditSaleModalComponent from "./EditSaleModalComponent";
import axios from "axios";
import { Url, Controllers } from "../../../../../../constants/UrlConstants";
import { NotificationContext } from "../../../../../../contexts/NotificationContext";
import getAntiForgeryAxiosConfig from "../../../../../../functions/getAntiForgeryAxiosConfig";
import hideModal from "../../../../../../functions/hideModal";
import {GradeOptions} from "../../../../../../constants/GradeOptions";

class EditSaleModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      userAddresses: [],
      descriptionInput: "",
      vinylGradeInput: "0",
      sleeveGradeInput: "0",
      priceInput: 0,
      shipsFromAddressSelectInput: ""
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    const sale = this.props.data.sale;
    this.setState({
      descriptionInput: sale.description,
      vinylGradeInput: sale.vinylGrade,
      sleeveGradeInput:sale.sleeveGrade,
      priceInput:sale.price,
    });
    this.handleLoadUserAddresses();
  }

  handleLoadUserAddresses = () => {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.addresses.name +
          Controllers.addresses.actions.getUserAddresses
      )
      .then(response => {
        this.setState({
          userAddresses: response.data.map(addressObj => {
            return {
              id: addressObj.id,
              name: `${addressObj.country}-${addressObj.town}-${addressObj.postalCode}-${addressObj.fullAddress}`
            };
          }),
          isLoading: false
        });
        this.context.handleAppNotification("Loaded user addresses", 5);
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to load user addresses!"
        );
      });
  };

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = event => {
    event.preventDefault();

    const submitFormObj = {
      saleId: this.props.data.sale.id,
      description: this.state.descriptionInput,
      vinylGrade: this.state.vinylGradeInput,
      sleeveGrade: this.state.sleeveGradeInput,
      price: this.state.priceInput,
      shipsFromAddressId: this.state.shipsFromAddressSelectInput
    };

    axios
      .put(
        Url.api + Controllers.sales.name + Url.slash,
        submitFormObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification("Sucessfully edited sale", 4);
        hideModal();
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to edit sale!"
        );
      });
  };

  handleFlushModal = () => {
    hideModal();
  };

  render() {
    return (
      <EditSaleModalComponent
        data={{
          descriptionInput: this.state.descriptionInput,
          vinylGradeInput: this.state.vinylGradeInput,
          sleeveGradeInput: this.state.sleeveGradeInput,
          priceInput: this.state.priceInput,
          userAddresses: this.state.userAddresses,
          shipsFromAddressSelectInput: this.state.shipsFromAddressSelectInput,
          gradeOptions:GradeOptions
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit,
          handleFlushModal: this.handleFlushModal
        }}
      />
    );
  }
}

export default EditSaleModalContainer;
