"""
Calculates the intersection point of line segments.

A solution to reddit's /r/dailyprogrammer challenge #163 found here:
http://www.reddit.com/r/dailyprogrammer/comments/26b42x/5232014_challenge_163_hard_intersecting_lines_in/

Usage: python challenge163.py segments.txt

Author: Tom Faris <ta.faris@gmail.com>
"""
import sys
    
    
class NamedSegment(object):
    """
    A line segment defined by four points and a name.
    """
    def __init__(self, name, x1, y1, x2, y2):
        self.name = name
        self.x1 = x1
        self.y1 = y1
        self.x2 = x2
        self.y2 = y2        
        
    def intersect(self, segment):
        """
        Returns x and y coordinates of the intersecting point of this 
        and the specified segment, or None if the two segments do not
        intersect.
        
        Algorithm borrowed from here: http://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect#answer-1968345
        """
        p0_x, p1_x, p2_x, p3_x = self.x1, self.x2, segment.x1, segment.x2
        p0_y, p1_y, p2_y, p3_y = self.y1, self.y2, segment.y1, segment.y2
        s1_x = p1_x - p0_x
        s1_y = p1_y - p0_y
        s2_x = p3_x - p2_x     
        s2_y = p3_y - p2_y

        try:
            s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y)
            t = ( s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y)

            if s >= 0 and s <= 1 and t >= 0 and t <= 1:        
                # Collision detected
                i_x = p0_x + (t * s1_x)            
                i_y = p0_y + (t * s1_y)
                return i_x, i_y
            return None # No collision
        except ZeroDivisionError:
            return None
        
        
class Intersection(object):
    """
    Represents an intersection between two line segments and the point
    at which they intersect.
    """
    def __init__(self, seg_a, seg_b, p):
        self.seg_a = seg_a
        self.seg_b = seg_b
        self.point = p
    
    
def parse(segment_file):
    segments = []
    i = 0
    for line in segment_file:
        segment, x1, y1, x2, y2 = line.split(" ")
        try:
            segments.append(NamedSegment(segment, float(x1), float(y1), float(x2), float(y2)))
        except ValueError:
            print 'Error parsing segment on line %s' % i+1
        i += 1
    return tuple(segments)
    

def check_intersections(segments):
    """
    Returns a tuple of segment intersections in the format:
    ((SEGMENT, (INTERSECTION1, INTERSECTION2, ...)),
     (SEGMENT, (...,)))
    """
    intersections = []
    for i in range(0, len(segments)):
        segment_a = segments[i]
        s = []
        for segment_b in segments[i+1:]:
            p = segment_a.intersect(segment_b)
            if p:
                s.append(Intersection(segment_a, segment_b, p))
        intersections.append((segment_a, tuple(s)))
    return tuple(intersections)
    
    
if __name__ == '__main__':
    # Load segments from the specified file and run them for
    # intersections with each other.
    segment_filename = sys.argv[1]
    f = open(segment_filename)
    try:
        segments_with_intersects = []
        intersects = check_intersections(parse(f))
        for i in intersects:
            seg_int = i[1]
            if len(seg_int) == 0 and i[0] not in segments_with_intersects:
                print '%s has no intersections.' % i[0].name
            else:
                if len(seg_int) > 0:
                    print 'Segment %s intersects: ' % i[0].name
                    for intersection in seg_int:
                        print '    %s @ (%.2f, %.2f)' % (
                            intersection.seg_b.name,
                            intersection.point[0],
                            intersection.point[1]
                        )
                        segments_with_intersects.append(intersection.seg_b)
    finally:
        f.close()
    