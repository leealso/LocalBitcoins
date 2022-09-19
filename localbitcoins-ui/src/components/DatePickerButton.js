import PropTypes from 'prop-types';
import DatePicker from 'react-datepicker';
import Button from 'react-bootstrap/Button';
import { forwardRef } from 'react';
import { Container } from 'react-bootstrap';

const DatePickerButton = ({ date, onDateChange }) => {
    const CustomInput = forwardRef(({ value, onClick }, ref) => (
        <Button className="float-right" onClick={onClick} ref={ref}>
            {value}
        </Button>
    ));
    return (
        <Container className="d-flex h-100 align-items-center float-right">
            <DatePicker
                selected={date}
                onChange={(date) => onDateChange(date)}
                customInput={<CustomInput className='text-end'/>}
                className='text-end'
            />
        </Container>
    );
}

DatePickerButton.defaultProps = {
    date: new Date().getTime()
}

DatePickerButton.propTypes = {
    date: PropTypes.number.isRequired
}

export default DatePickerButton;