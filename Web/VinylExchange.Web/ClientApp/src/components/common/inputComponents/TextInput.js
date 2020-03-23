import React, { Fragment, Component } from "react";
import uuid4 from "./../../../functions/guidGenerator";

const alphaNumericRegex = new RegExp("^[A-Za-z0-9-() ]*$");

class TextInput extends Component {
  constructor(props) {
    super(props);

    this.state = {
      validationRules: {
        isRequired: false,
        isAlphaNumeric:false,
        isValidateLength: false,
        minLength: 0,
        maxLength: 0
      },
      validationMessages: []
    };
  }

  componentDidMount() {
    if (this.props.required === true) {
      this.setState({
        validationRules: {
          isRequired: true
        }
      });
    }

    if (this.props.validateLength === true) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            isValidateLength: true,
            minLength: this.props.minLength,
            maxLength: this.props.maxLength
          }
        };
      });
    }

    if (this.props.alphanumeric=== true) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            isAlphaNumeric:true
          }
        };
      });
    }
  }

  onChange = event => {
    this.props.onChange(event);
    this.InputValidation(event.target.value);
  };

  InputValidation = value => {
    const validationRules = this.state.validationRules;

    const updatedValidationMessages = [];

    if (validationRules.isRequired) {
      if (value.length === 0) {
        updatedValidationMessages.push("This field is required");
      }
    }

    if (validationRules.isValidateLength) {
      if (
        value.length < validationRules.minLength ||
        value.length > validationRules.maxLength
      ) {
        updatedValidationMessages.push(
          `You must enter between ${validationRules.minLength} and ${validationRules.maxLength} characters`
        );
      }
    }

    if(validationRules.isAlphaNumeric){
        if(!alphaNumericRegex.test(value)){
          updatedValidationMessages.push(
            `Allowed characters are A-Z,a-z,0-9,(,),-,whitespace`
          );
        }
    }

    this.setState({ validationMessages: updatedValidationMessages });
  };

  render() {
    const extraClasses =
      this.props.extraClasses === undefined
        ? ""
        : " " + this.props.extraClasses;

    const validationMessages = this.state.validationMessages.map(
      validationMsg => {
        return (
          <small
            id="passwordHelpBlock"
            className="form-text text-danger"
            
            key={uuid4()}
          >
            -{validationMsg}
          </small>
        );
      }
    );
    return (
      <div className="text-left">
        <input
          type="text"
          className={"form-control" + extraClasses}
          id={this.props.id}
          value={this.props.value}
          name={this.props.id}
          onFocus={event => {
            this.InputValidation(event.target.value);
          }}
          onChange={this.onChange}
          placeholder={this.props.placeholder}
        />
        {validationMessages}
      </div>
    );
  }
}

export default TextInput;
