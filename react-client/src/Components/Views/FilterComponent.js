import { useFormik } from 'formik';
import { Select, MenuItem } from '@mui/material';
import axios from 'axios';

export const FilterComponent = (props) => {
    const formik = useFormik({
        initialValues: {
            sort: 0,
        },
        onSubmit: (values) => {
            axios({
                method: 'post',
                url: 'https://localhost:44332/listing/sort',
                data: values
            })
            .then((response) => {
                props.updateListings(response.data);             
            })
        }
    });

    return (
        <div className="filter-top-container">
            <label htmlFor="sort">Sort By</label>
            <Select
                name="sort"
                onChange={(e) => {formik.handleChange(e); formik.submitForm(formik.values);}}
                value={formik.values.sort}
            >
                <MenuItem value={0}>Maximum price</MenuItem>
                <MenuItem value={1}>Number of roommates</MenuItem>
                <MenuItem value={2}>City</MenuItem>
            </Select>
        </div>
    );
}