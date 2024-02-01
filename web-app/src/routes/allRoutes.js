import { Routes, Route } from 'react-router-dom';

import {SearchNewsPage} from '../pages/index';

export const AllRoutes = () => {
  return (
    <Routes>
        <Route path="/" exact element={ <SearchNewsPage/> } />
      </Routes>
  );
}