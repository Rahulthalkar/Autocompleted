import { Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { AutoCompleteComponent } from './pages/auto-complete/auto-complete.component';

export const routes: Routes = [
    { path: 'search', component: SearchComponent },
    { path: '**', component: AutoCompleteComponent },
];
