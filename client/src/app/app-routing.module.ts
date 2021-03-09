import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [  //This array to provide the routes that we tell Angular about: Mảng này sẽ cung cấp các định tuyến mà chúng ta cho Angular biết
  { path: '', component: HomeComponent }, // when we say empty string, when somebody browses to localhost for 200 directly
  // then this is component that is going to be loaded
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: '', component: HomeComponent },
      { path: 'members', component: MemberDetailComponent },
      { path: 'members/:id', component: MemberDetailComponent },
      { path: 'lists', component: MemberListComponent },
      { path: 'messages', component: MessagesComponent }
    ]
  },
  { path: 'errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: ServerErrorComponent, pathMatch: 'full' } // When user's typed in something that doesn't match, we are going to redirects
  // them back to the home component
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }