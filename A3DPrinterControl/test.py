import numpy

def f(x):
	y=1+x
	import math
	return math.sin(y)

def e(ary):
	a = numpy.array(ary)
	return a.shape

def t(sct):
	return "{0}{1}, {2}{3}, {4}{5}, {6}".format(sct.item1, type(sct.item1), 
										sct.item2, type(sct.item2),
										sct.item3, type(sct.item3),
										type(sct))

def ty(t):
	return "{0}:\n{1}".format(type(t), t[0])

def tu():
	return (1, 2, 3)