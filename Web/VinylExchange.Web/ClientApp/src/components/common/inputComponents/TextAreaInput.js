import React, { Component } from "react";
import uuid4 from "./../../../functions/guidGenerator";
import InputValidationMessage from "./../clienSideValidation/InputValidationMessage";

const AplhaNumericBracesDashAndSpaceRegex = new RegExp(/^[A-Za-z0-9-() ]*$/);
const AlphaNumericAndUnderscoreRegex = new RegExp(/^[A-Za-z0-9_]*$/);
const AlphaNumericDotCommaAndSpaceRegex = new RegExp(/^[A-Z-a-z0-9., ]*$/);
const LettersOnlyRegex = new RegExp(/^[A-Za-z]*$/);
const ValidateEmailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

class TextAreaInput extends Component {
  constructor(props) {
    super(props);

    this.state = {
      validationRules: {
        isRequired: false,
        isAplhaNumericBracesDashAndSpace: false,
        isAlphaNumericAndUnderscore: false,
        isAlphaNumericDotCommaAndSpace: false,
        isLettersOnly: false,
        isValidateEmail:false,
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

    if (this.props.validateEmail === true) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            isValidateEmail:true
          }
        };
      });
    }


    if (this.props.aplhaNumericBracesDashAndSpace === true) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            isAplhaNumericBracesDashAndSpace: true
          }
        };
      });
    } else if (this.props.alphaNumericAndUnderscore === true) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            isAlphaNumericAndUnderscore: true
          }
        };
      });
    } else if (this.props.alphaNumericDotCommaAndSpace === true) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            isAlphaNumericDotCommaAndSpace: true
          }
        };
      });
    } else if (this.props.lettersOnly === true) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            isLettersOnly: true
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
        updatedValidationMessages.push("This field is required!");
      }
    }

    if (validationRules.isValidateLength) {
      if (
        value.length < validationRules.minLength ||
        value.length > validationRules.maxLength
      ) {
        updatedValidationMessages.push(
          `You must enter between ${validationRules.minLength} and ${validationRules.maxLength} characters!`
        );
      }
    }

    if (validationRules.isAplhaNumericBracesDashAndSpace) {
      if (!AplhaNumericBracesDashAndSpaceRegex.test(value)) {
        updatedValidationMessages.push(
          "Allowed characters are (A-Z,a-z,0-9,(,),-,whitespace)!"
        );
      }
    }

    if (validationRules.isAlphaNumericAndUnderscore) {
      if (!AlphaNumericAndUnderscoreRegex.test(value)) {
        updatedValidationMessages.push(
          "Allowed characters are (A-Z,a-z,0-9,_)!"
        );
      }
    }

    if (validationRules.isAlphaNumericDotCommaAndSpace) {
      if (!AlphaNumericDotCommaAndSpaceRegex.test(value)) {
        updatedValidationMessages.push(
          "Allowed characters are (A-Z,a-z,0-9,.,comma, )!"
        );
      }
    }

    if (validationRules.isLettersOnly) {
      if (!LettersOnlyRegex.test(value)) {
        updatedValidationMessages.push(
          "Allowed characters are (A-Z,a-z)!"
        );
      }
    }

    if(validationRules.isValidateEmail){
      if(!ValidateEmailRegex.test(value)){
        updatedValidationMessages.push(
          "Please enter a valid e-mail! address"
        );
      }
    }

    this.setState({ validationMessages: updatedValidationMessages });
  };

  render() {

    const validationMessages = this.state.validationMessages.map(
      validationMsg => {
        return <InputValidationMessage message={validationMsg} key={uuid4()} />;
      }
    );
    return (
      <div className="text-left">
      <textarea
        className="form-control"
        id={this.props.id}
        name={this.props.id}
        rows={this.props.rows}
        onFocus={event => {
          this.InputValidation(event.target.value);
        }}
        onChange={this.onChange}
        value={this.props.value}
      ></textarea>
      {validationMessages}
      </div>
    );
  }
}

export default TextAreaInput;
