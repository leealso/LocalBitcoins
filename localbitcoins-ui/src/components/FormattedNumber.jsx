import PropTypes from 'prop-types';

const FormattedNumber = ({ text, prefix, decimals, thousandSeparator }) => {
    return (
        <span>
            {
                `${prefix}`
                + (thousandSeparator 
                    ? text.toFixed(decimals).replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)
                    : text.toFixed(decimals)
                )
            }
        </span>
    )
};

FormattedNumber.defaultProps = {
    text: '',
    prefix: '',
    thousandSeparator: true,
    decimals: 2
};

FormattedNumber.propTypes = {
    text: PropTypes.number.isRequired,
    prefix: PropTypes.string,
    decimals: PropTypes.number
};

export default FormattedNumber;