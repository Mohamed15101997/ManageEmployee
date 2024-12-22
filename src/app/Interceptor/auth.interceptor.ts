import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  let token = localStorage.getItem('token');
  let cloned = req.clone({
    setHeaders :{
      Authorization:`Bearer ${token}`
    }
  });
  return next(cloned);
};
