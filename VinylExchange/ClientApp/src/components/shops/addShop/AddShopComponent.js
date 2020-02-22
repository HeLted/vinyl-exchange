import React, { Component } from "react";
import Label from "./../../common/inputComponents/Label";
import TextInput from "./../../common/inputComponents/TextInput";
import SingleSelect from "./../../common/inputComponents/SingleSelect";
import Select, { components } from "react-select";
import { Url, Controllers } from "./../../../constants/UrlConstants";
import $script from "scriptjs";
import { Helmet } from "react-helmet";

const shopTypeOptions = [
  { id: 1, name: "Web Shop" },
  { id: 2, name: "Physical Shop" },
  { id: 3, name: "Web/Physical Shop" }
];

class AddShopComponent extends Component {
  constructor() {
    super();
  }

  componentDidMount() {
    $script.get("/js/dropzone.js", function() {
      $script.get("/js/imagesDropzoneScript.js", function() {});
    });
  }

  componentWillUnmount() {
    var head = document.getElementsByTagName("head")[0];
    head.removeChild(document.getElementById("imagesDropzoneScript"));
  }

  render() {
    const physicalShopInputs =
      this.props.state.shopTypeSelectInput === "2" ||
      this.props.state.shopTypeSelectInput === "3" ? (
        <div className="row">
          <div className="col">
            <div className="form-group">
              <Label for="countryInput" value="Country" />
              <TextInput
                id="countryInput"
                placeholder="Country..."
                value={this.props.state.countryInput}
                onChange={this.props.functions.handleOnChange}
              />
            </div>
          </div>
          <div className="col">
            <div className="form-group">
              <Label for="townInput" value="Town" />
              <TextInput
                id="townInput"
                placeholder="Town..."
                value={this.props.state.townInput}
                onChange={this.props.functions.handleOnChange}
              />
            </div>
          </div>
          <div className="col">
            <div className="form-group">
              <Label for="addressInput" value="Address" />
              <TextInput
                id="addressInput"
                placeholder="Address..."
                value={this.props.state.addressInput}
                onChange={this.props.functions.handleOnChange}
              />
            </div>
          </div>
        </div>
      ) : null;

    return (
      <div className="release-form">
        <Helmet>
          <link rel="stylesheet" href="/css/dropzone.css" />
        </Helmet>
        <form onSubmit={event => this.props.functions.handleOnSubmit(event)}>
          <div className="row">
            <div className="col">
              <div className="form-group">
                <Label for="shopNameInput" value="ShopName" />
                <TextInput
                  id="shopNameInput"
                  placeholder="Shop Name..."
                  value={this.props.state.shopNameInput}
                  onChange={this.props.functions.handleOnChange}
                />
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col">
              <div className="form-group">
                <Label for="shopTypeSelectInput" value="Shop Type" />
                <SingleSelect
                  id="shopTypeSelectInput"
                  value={this.props.state.shopTypeSelectInput}
                  onChange={this.props.functions.handleOnChange}
                  options={shopTypeOptions}
                  defaultOptionLabel="--Please Select Shop Type--"
                />
              </div>
            </div>

            <div className="col">
              <div className="form-group">
                <Label for="webAddressInput" value="Website Address" />
                <TextInput
                  id="webAddressInput"
                  placeholder="Website Address..."
                  value={this.props.state.formatInput}
                  onChange={this.props.functions.handleOnChange}
                />
              </div>
            </div>
          </div>

          {physicalShopInputs}

          <div className="form-group">
            <Label for="release-images-dropzone" value="Shop Images" />
            <div
              id="imagesDropzone"
              formsessionid={this.props.state.formSessionId}
              dropzoneuploadpath={Url.api + Controllers.files.name}
              dropzonedeletepath={Url.api + Controllers.files.name + Url.slash}
              acceptedfiles=".png, .jpg"
            ></div>
          </div>

          <button type="submit" className="btn btn-primary">
            Submit
          </button>
        </form>
      </div>
    );
  }
}

export default AddShopComponent;
